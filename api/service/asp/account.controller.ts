import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';



export class AccountController {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }


    generateTs() {
        const ACCOUNTSCONTROLLER_CS = 'AccountsController.cs';
        // else if (config.initFiles && file === ACCOUNTSCONTROLLER_CS) {
        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${ACCOUNTSCONTROLLER_CS}`, 'utf8');
        // edit content

        const user = this.configs.classes.find(e => e.class.includes('user') || e.class.includes('utilisateur'));

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

        fse.ensureDirSync(`${this.configs.aspFolder}/Controllers`);
        fse.writeFileSync(`${this.configs.aspFolder}/Controllers/${ACCOUNTSCONTROLLER_CS}`, content);
        this.helper.progress(`>> ${ACCOUNTSCONTROLLER_CS} done`);
        // }
    }
}