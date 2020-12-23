import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';

export class ModelsHandler {

    constructor(private helper: HelperFunctions, private configs: IConfigs) { }

    generateTs() {
        const MODELS_TS = 'models.ts';
        const publicPath = `${this.configs.pathAbs}/api/public`;



        const user = this.configs.classes.find(e => e.class.includes('user') || e.class.includes('utilisateur'));

        if (user === null) {
            const cls = `
            export class User {
                id = 0;
                email = '';
                password = '';
                isActive = false;
                emailVerified = false;
                codeOfVerification = '';
            }\r\n\r\n
            `;
            let content = fse.readFileSync(`${publicPath}/${MODELS_TS}`, 'utf8');

            content = cls + content;

            fse.writeFileSync(`${publicPath}/${MODELS_TS}`, content);
            
            console.log(`>> ${publicPath}/${MODELS_TS} done`);
        } else { }

        if (this.configs.replaceModels) {
            fse.copySync(`${this.configs.pathAbs}/api/public/${MODELS_TS}`, `${this.configs.angularAppFolder}/models/${MODELS_TS}`)
        }
    }

    propertyToCamelCase() {}

}
