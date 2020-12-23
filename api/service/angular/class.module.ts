import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';


const CLASS_MODULE_TS = 'class.module.ts';
const USER_ROUTING_MODULE_TS = 'class-routing.module.ts';

export class ClassModule {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }


    generateTs() {
        // get content
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${CLASS_MODULE_TS}`, 'utf8');
        let contentR = fse.readFileSync(`${this.configs.pathBaseFiles}/${USER_ROUTING_MODULE_TS}`, 'utf8');

        this.configs.classes.forEach(e => {
            // write content in new location
            const moduleName = this.helper.moduleName(this.configs.modules, e.class);
            const path = moduleName ? `${adminFolder}/${moduleName}` : adminFolder;

            let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
            newContent = newContent.replace(/user/g, e.class);


            fse.ensureDirSync(`${path}/${e.class}`);
            fse.writeFileSync(`${path}/${e.class}/${e.class}.module.ts`, newContent);

            // generate routing

            let newContentR = contentR.replace(/User\$/g, this.helper.Cap(e.class));
            newContentR = newContentR.replace(/user/g, e.class);

            fse.ensureDirSync(`${path}/${e.class}`);
            fse.writeFileSync(`${path}/${e.class}/${e.class}-routing.module.ts`, newContentR);
        });
    }
}