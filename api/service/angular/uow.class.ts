import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';

const UOW_SERVICE_TS = 'uow.service.ts';
const CLASS_SERVICE_TS = 'class.service.ts';

export class UowClass {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }


    generateTs() {
        // else if (file === UOW_SERVICE_TS) { // and services
            const distination = `${this.configs.angularAppFolder}/services`;
            fse.ensureDirSync(distination);

            let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${UOW_SERVICE_TS}`, 'utf8');

            let contentService = fse.readFileSync(`${this.configs.pathBaseFiles}/${CLASS_SERVICE_TS}`, 'utf8');

            let imports = '';
            let services = '';
            // edit content
            this.configs.classes.forEach(e => {
                let params = '';
                let params2 = '';
                imports += `import { ${this.helper.Cap(e.class)}Service } from './${e.class}.service';\r\n`;
                services += `${e.class}s = new ${this.helper.Cap(e.class)}Service();\r\n`;

                e.properties.forEach(p => {
                    const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                    if (isTypePrimitive && p.name.toLowerCase() !== 'id' && p.type !== 'Date' && p.type !== 'boolean'
                        && !p.name.startsWith('image') && !p.name.startsWith('desc') && !p.name.includes('pass')) {

                        params += `${p.name}, `;
                        params2 += `/\${${p.name}}`;
                    }
                });

                // content = content.replace('/*{imports}*/', imports);
                let newContentService = contentService.replace(/\/\*\{params\}\*\//g, params);
                newContentService = newContentService.replace(/\/\*\{params2\}\*\//g, params2);
                newContentService = newContentService.replace(/User\$/g, this.helper.Cap(e.class));
                newContentService = newContentService.replace(`('users')`, `('${e.class}s')`);

                // write content in new location
                fse.writeFileSync(`${distination}/${e.class}.service.ts`, newContentService);
                this.helper.progress(`>> ${e.class}.service.ts done`);
            });

            content = content.replace('/*{imports}*/', imports);
            content = content.replace('/*{services}*/', services);
            // write content in new location
            fse.writeFileSync(`${distination}/${UOW_SERVICE_TS}`, content);
            this.helper.progress(`>> ${UOW_SERVICE_TS} done`);
        // }
    }
}