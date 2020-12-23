import * as fse from 'fs-extra';
import { ClassReader } from './class-reader';
import { Container } from 'typedi';
import { HelperFunctions, Model, IConfig } from './helper.functions';

const DATASEEDING_CS = 'DataSeeding.cs';
const MYCONTEXT_CS = 'MyContext.cs';
const ACCOUNTSCONTROLLER_CS = 'AccountsController.cs';
const USERSCONTROLLER_CS = 'UsersController.cs';
export class GenerateAsp {

    private helper: HelperFunctions = Container.get(HelperFunctions);

    constructor() { }

    methode(config: IConfig, MODELS_TS = 'models.ts') {
        const pathAbs = !process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`
        const primitivetypes = ['string', 'boolean', 'Date', 'number'];
        const source = `${pathAbs}/api/base/source/asp`;
        const asp = `${pathAbs}/api/base/asp`;
        const admin_folder = 'admin';
        const angular_app = `${asp}/angular/src/app`;
        const files = fse.readdirSync(`${source}`);

        const classes: Model[] = new ClassReader().methode(MODELS_TS);
        // return;
        files.forEach(file => {

            if (file === DATASEEDING_CS) {
                // get content
                const distination = `${asp}/angular/src/app/${admin_folder}`;
                let content = fse.readFileSync(`${source}/${DATASEEDING_CS}`, 'utf8');
                let dataSeed = '';
                // edit content
                classes.forEach(e => {
                    let seed =
                        `public static ModelBuilder ${this.helper.Cap(e.class)}s(this ModelBuilder modelBuilder)
                        {
                        int id = 1;
                        var faker = new Faker<${this.helper.Cap(e.class)}>(DataSeeding.lang)
                            .CustomInstantiator(f => new ${this.helper.Cap(e.class)} { Id = id++ })\r\n`;

                    e.properties.forEach(p => {
                        const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                        if (isTypePrimitive && p.name.toLowerCase() !== 'id') {
                            const isDate = p.type === 'Date';
                            const isInt = p.type === 'number';
                            const isBoolean = p.type === 'boolean';
                            const isEmail = p.name.toLowerCase().includes('email');
                            const isImage = p.name.toLowerCase().includes('image');
                            const isPassword = p.name.toLowerCase().includes('pass');
                            // const isString = p.type === 'string';
                            switch (true) {
                                case isDate: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => f.Date.Past())\r\n`; break;
                                case isInt: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => f.Random.Number(1, 10))\r\n`; break;
                                case isBoolean: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => id - 1 == 1 ? true : f.Random.Bool())\r\n`; break;
                                case isEmail: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => id - 1 == 1 ? "dj-m2x@hotmail.com" : f.Internet.Email())\r\n`; break;
                                case isImage: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => "")\r\n`; break;
                                case isPassword: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => "123")\r\n`; break;
                                // case isString: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => f.Lorem.Word())`;break;
                                default: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => f.Lorem.Word())\r\n`; break;
                            }
                        }

                    });

                    seed += `;\r\nmodelBuilder.Entity<${this.helper.Cap(e.class)}>().HasData(faker.Generate(10));\r\nreturn modelBuilder;\r\n}\r\n\r\n`;

                    const containeForeignKey = e.properties.findIndex(o => o.name.toLowerCase() !== 'id' && o.name.startsWith('id')) !== -1;

                    containeForeignKey ? dataSeed += seed : dataSeed = seed + dataSeed;
                });

                let newContent = content.replace('/*{dataSeed}*/', dataSeed);

                // write content in new location
                fse.ensureDirSync(`${asp}/Models`);
                fse.writeFileSync(`${asp}/Models/${DATASEEDING_CS}`, newContent);
                this.helper.progress(`>> Models/${DATASEEDING_CS} done`);
            }


            else if (file === MYCONTEXT_CS) {
                let content = fse.readFileSync(`${source}/${MYCONTEXT_CS}`, 'utf8');
                let entities = '';
                let models = '';
                let dbSets = '';
                let seedClass = '';

                fse.ensureDirSync(`${asp}/Models`);

                classes.forEach(e => {
                    dbSets += `public virtual DbSet<${this.helper.Cap(e.class)}> ${this.helper.Cap(e.class)}s { get; set; } \r\n`;

                    entities += `modelBuilder.Entity<${this.helper.Cap(e.class)}>(entity => \r\n{`;


                    models = `using System;\r\nusing System.Collections.Generic;\r\nnamespace Models\r\n{\r\npublic partial class ${this.helper.Cap(e.class)} \r\n{`;
                    let l = [];
                    e.properties.forEach(p => {
                        const isTypePrimitive = this.helper.isTypePrimitive(p.type);
                        if (isTypePrimitive) {
                            if (p.name.toLowerCase() === 'id') {
                                entities += `entity.HasKey(e => e.${this.helper.Cap(p.name)});\r\n`;
                                entities += `entity.Property(e => e.${this.helper.Cap(p.name)}).ValueGeneratedOnAdd();\r\n`;

                                models += `public int ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                            } else if (p.name.toLowerCase() === 'email') {
                                entities += `entity.HasIndex(e => e.${this.helper.Cap(p.name)}).IsUnique();\r\n`; // .IsRequired(false)
                                models += `public string ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                            } else {
                                entities += `entity.Property(e => e.${this.helper.Cap(p.name)});\r\n`; // .IsRequired(false)
                                const type = p.type === 'Date' ? 'DateTime' : (p.type === 'number' ? 'int' : (p.type === 'boolean' ? 'bool' : p.type));
                                models += `public ${type} ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                            }
                        } else {
                            if (p.type.includes('[]')) {


                                const pr: { name: string, type: string } = this.helper.getNameFor_withOne_ef_relation(e.class, p, classes);
                                // console.log(pr)
                                entities += `entity.HasMany(d => d.${this.helper.Cap(p.name)}).WithOne(p => p.${this.helper.Cap(pr.name)}).HasForeignKey(d => d.Id${this.helper.Cap(pr.name)}).OnDelete(DeleteBehavior.NoAction);\r\n`;

                                const cls = p.type.replace('[]', '');
                                models += `public virtual ICollection<${this.helper.Cap(cls)}> ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                            } else {
                                const pr: { name: string, type: string } = this.helper.getNameFor_HasOne_ef_relation(e.class, p, classes);

                                entities += `entity.HasOne(d => d.${this.helper.Cap(p.name)}).WithMany(p => p.${this.helper.Cap(pr.name)}).HasForeignKey(d => d.Id${this.helper.Cap(p.name)});\r\n`;

                                models += `public virtual ${this.helper.Cap(p.type !== 'any' ? p.type : p.name)} ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                            }
                        }

                    });

                    entities += '});\r\n\r\n';
                    models += '}\r\n}\r\n';

                    // for reooder things
                    const containeForeignKey = e.properties.findIndex(o => o.name.toLowerCase() !== 'id' && o.name.startsWith('id')) !== -1;
                    containeForeignKey ? seedClass += `.${this.helper.Cap(e.class)}s()\r\n` : seedClass = `.${this.helper.Cap(e.class)}s()\r\n` + seedClass;

                    fse.writeFileSync(`${asp}/Models/${this.helper.Cap(e.class)}.cs`, models);
                });
                // content = content.replace('/*{imports}*/', imports);
                content = content.replace('/*{entities}*/', entities);
                content = content.replace('/*{dbSets}*/', dbSets);
                content = content.replace('/*{seedClass}*/', seedClass);
                // write content in new location

                fse.writeFileSync(`${asp}/Models/${MYCONTEXT_CS}`, content);
                this.helper.progress(`>> ${MYCONTEXT_CS} done`);

                // create models


            }

            else if (file === USERSCONTROLLER_CS) {
                let content = fse.readFileSync(`${source}/${USERSCONTROLLER_CS}`, 'utf8');
                // edit content
                classes.forEach(e => {
                    let whereClause = '';
                    let params = '';
                    let params2 = '';
                    let includes = '';
                    let select = `.Select(e => new \r\n${this.helper.spaceTab(4)}{\r\n${this.helper.spaceTab(5)}`;

                    e.properties.forEach(p => {
                        const isTypePrimitive = this.helper.isTypePrimitive(p.type);
                        // for search matter
                        const isID = p.name.toLowerCase() === 'id';
                        const isDate = p.type === 'Date';
                        const isBool = p.type === 'boolean';
                        const isImage = p.name.startsWith('image');
                        const isDescription = p.name.startsWith('desc');
                        const isPassword = p.name.includes('pass');
                        const isPropertyNav = p.name.toLowerCase() !== 'id' && p.name.startsWith('id');

                        if (isTypePrimitive) {

                            if (!isID && !isDate && !isBool && !isImage && !isDescription && !isPassword) {
                                // special case
                                const name = p.name !== 'action' ? p.name : 'action_';
                                // console.log(name)
                                params += `/{${name}}`;
                                params2 += `${p.type === 'number' ? 'int' : p.type} ${name}, `;

                                if (p.type === 'number') {
                                    whereClause += `.Where(e => ${name} == 0 ? true : e.${this.helper.Cap(p.name)} == ${name})\r\n`;
                                } else {
                                    whereClause += `.Where(e => ${name} == "*" ? true : e.${this.helper.Cap(p.name)}.ToLower().Contains(${name}.ToLower()))\r\n`;
                                }
                            }

                            

                            if (isPropertyNav) {
                                const { classNav, displayproperty, type } = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
                                // includes += `.Include(e => e.${this.helper.Cap(classNav)})`;
                                select += `${classNav} = e.${this.helper.Cap(classNav)}.${this.helper.Cap(displayproperty)},\r\n`;
                                select += `id${this.helper.Cap(classNav)} = e.Id${this.helper.Cap(classNav)},\r\n`;
                            } else {
                                select += `${p.name} = e.${this.helper.Cap(p.name)},\r\n${this.helper.spaceTab(5)}`;
                            }
                        }
                    });

                    select += `\r\n${this.helper.spaceTab(5)}})`

                    // params = params.substring(0, params.lastIndexOf(','))
                    params2 = params2.substring(0, params2.lastIndexOf(','))
                    // content = content.replace('/*{imports}*/', imports);
                    let newContent = content.replace(/\/\*\{params\}\*\//g, params);
                    newContent = newContent.replace(/\/\*\{params2\}\*\//g, params2);
                    newContent = newContent.replace(/\/\*\{whereClause\}\*\//g, whereClause);
                    newContent = newContent.replace('/*{includes}*/', includes);
                    newContent = newContent.replace('/*{select}*/', select);

                    newContent = newContent.replace(/UserX/g, this.helper.Cap(e.class));

                    if (e.properties.length >= 5) {
                        content = content.replace(/\/\/\>State/g, '');
                        content = content.replace(/\/\/\<State/g, '');
                    } else {
                        const length = (content.match(/\/\/\>State/g) || []).length;
                        for (let i = 0; i < length; i++) {
                            content = this.helper.removeZoneOfText(content, '//>State', '//<State');
                        }
                    }
                    // write content in new location

                    fse.ensureDirSync(`${asp}/Controllers`);

                    fse.writeFileSync(`${asp}/Controllers/${this.helper.Cap(e.class)}Controller.cs`, newContent);
                    this.helper.progress(`>> ${this.helper.Cap(e.class)}Controller.cs done`);
                });

                // const distination = `${asp}/Controllers`;
                // fse.ensureDirSync(distination);
                // fse.copySync(`${source}/${SUPERCONTROLLER_CS}`, `${distination}/${SUPERCONTROLLER_CS}`)
            }

            else if (config.initFiles && file === ACCOUNTSCONTROLLER_CS) {
                let content = fse.readFileSync(`${source}/${ACCOUNTSCONTROLLER_CS}`, 'utf8');
                // edit content

                const user = classes.find(e => e.class.includes('user') || e.class.includes('utilisateur'));

                if (user) {
                    const addEmailFeature = user.properties.find(p => p.name.includes('codeOfVerification') && p.name.includes('emailVerified'))
                    const containeIsActiveProp = user.properties.find(p => p.name.includes('isActive')) !== null;

                    if (addEmailFeature) {
                        content = content.replace(/\/\/\>Email/g, '');
                        content = content.replace(/\/\/\<Email/g, '');
                    } else {
                        const length = (content.match(/\/\/\>Email/g) || []).length;
                        for (let i = 0; i < length; i++) {

                            content = this.helper.removeZoneOfText(content, '//>Email', '//<Email');

                        }
                    }

                    if (containeIsActiveProp) {
                        content = content.replace(/\/\/\>IsActive/g, '');
                        content = content.replace(/\/\/\<IsActive/g, '');
                    } else {
                        content = this.helper.removeZoneOfText(content, '//>IsActive', '//<IsActive');
                    }

                }


                fse.writeFileSync(`${asp}/Controllers/${ACCOUNTSCONTROLLER_CS}`, content);
                this.helper.progress(`>> ${ACCOUNTSCONTROLLER_CS} done`);
            }
        });
    }


}

