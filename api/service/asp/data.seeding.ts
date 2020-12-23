import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';


export class DataSeeding {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }


    generateTs() {
        const DATASEEDING_CS = 'DataSeeding.cs';

        // if (file === DATASEEDING_CS) {
        // get content
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        const distination = adminFolder;
        
        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${DATASEEDING_CS}`, 'utf8');
        let dataSeed = '';
        // edit content
        this.configs.classes.forEach(e => {
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
                        case isEmail: seed += `.RuleFor(o => o.${this.helper.Cap(p.name)}, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())\r\n`; break;
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
        fse.ensureDirSync(`${this.configs.aspFolder}/Models`);
        fse.writeFileSync(`${this.configs.aspFolder}/Models/${DATASEEDING_CS}`, newContent);
        this.helper.progress(`>> Models/${DATASEEDING_CS} done`);
        // }
    }
}