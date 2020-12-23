import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';

export class MyContext {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }


    generateTs() {
        const MYCONTEXT_CS = 'MyContext.cs';
        // else if (file === MYCONTEXT_CS) {
        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${MYCONTEXT_CS}`, 'utf8');
        let modelBuilderEntity = '';
        let models = '';
        let dbSets = '';
        let seedClass = '';

        fse.ensureDirSync(`${this.configs.aspFolder}/Models`);

        
        this.configs.classes.forEach(e => {
            const cap = this.helper.Cap(e.class);
            const dbsets = cap.endsWith('s') ? `${cap}es` : `${cap}s`

            dbSets += `public virtual DbSet<${this.helper.Cap(e.class)}> ${dbsets}s { get; set; } \r\n`;

            modelBuilderEntity += `modelBuilder.Entity<${this.helper.Cap(e.class)}>(entity => \r\n{`;


            models = `using System;\r\nusing System.Collections.Generic;\r\nnamespace Models\r\n{\r\npublic partial class ${this.helper.Cap(e.class)} \r\n{`;
            let l = [];

            e.properties.forEach(p => {
                const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                if (isTypePrimitive) {
                    if (p.name.toLowerCase() === 'id') {
                        modelBuilderEntity += `entity.HasKey(e => e.${this.helper.Cap(p.name)});\r\n`;
                        modelBuilderEntity += `entity.Property(e => e.${this.helper.Cap(p.name)}).ValueGeneratedOnAdd();\r\n`;

                        models += `public int ${this.helper.Cap(p.name)} { get; set; }\r\n`;

                    } else if (p.name.toLowerCase() === 'email') {
                        modelBuilderEntity += `entity.HasIndex(e => e.${this.helper.Cap(p.name)}).IsUnique();\r\n`; // .IsRequired(false)
                        models += `public string ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                    } else {
                        modelBuilderEntity += `entity.Property(e => e.${this.helper.Cap(p.name)});\r\n`; // .IsRequired(false)
                        const type = p.type === 'Date' ? 'DateTime' : (p.type === 'number' ? 'int' : (p.type === 'boolean' ? 'bool' : p.type));
                        models += `public ${type} ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                    }
                } else {
                    if (p.type.includes('[]')) {
                        // const pr: { name: string, type: string } = this.helper.getNameFor_withOne_ef_relation(e.class, p, this.configs.classes);
                        // console.log(pr)
                        modelBuilderEntity += `entity.HasMany(e => e.${this.helper.Cap(p.name)}).WithOne(p => p.${this.helper.Cap(e.class)}).HasForeignKey(e => e.${this.helper.Cap(e.class)}Id).OnDelete(DeleteBehavior.Cascade);\r\n`;

                        let cls = p.type.replace('[]', '');

                        cls = cls.toLowerCase() !== 'action' ? cls : cls + '_';

                        models += `public virtual ICollection<${this.helper.Cap(cls)}> ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                    } else {
                        const pr: { name: string, type: string } = this.helper.getNameFor_HasOne_ef_relation(e.class, p, this.configs.classes);

                        modelBuilderEntity += `entity.HasOne(e => e.${this.helper.Cap(p.name)}).WithMany(e => e.${dbsets}).HasForeignKey(e => e.${this.helper.Cap(p.name)}Id);\r\n`;

                        models += `public virtual ${this.helper.Cap(p.type !== 'any' ? p.type : p.name)} ${this.helper.Cap(p.name)} { get; set; }\r\n`;
                    }
                }

            });

            modelBuilderEntity += '});\r\n\r\n';
            models += '}\r\n}\r\n';

            // for reooder things
            const containeForeignKey = e.properties.findIndex(o => o.name.toLowerCase() !== 'id' && o.name.startsWith('id')) !== -1;
            containeForeignKey ? seedClass += `.${this.helper.Cap(e.class)}s()\r\n` : seedClass = `.${this.helper.Cap(e.class)}s()\r\n` + seedClass;

            fse.writeFileSync(`${this.configs.aspFolder}/Models/${this.helper.Cap(e.class)}.cs`, models);
        });
        // content = content.replace('/*{imports}*/', imports);
        content = content.replace('/*{entities}*/', modelBuilderEntity);
        content = content.replace('/*{dbSets}*/', dbSets);
        content = content.replace('/*{seedClass}*/', seedClass);
        // write content in new location

        fse.writeFileSync(`${this.configs.aspFolder}/Models/${MYCONTEXT_CS}`, content);
        this.helper.progress(`>> ${MYCONTEXT_CS} done`);

        // create models
        // }
    }
}