import { ClassReader } from './class-reader';
import * as fse from 'fs-extra';


export class HandleUserClass {


    constructor(MODELS_TS = 'models.ts', publicPath: string) {
        const classes: Model[] = new ClassReader().methode(MODELS_TS);


        const user = classes.find(e => e.class.includes('user') || e.class.includes('utilisateur'));

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

    }

    propertyToCamelCase() {}

}

interface Model {
    class: string;
    properties: { name: string; type: string; }[];
}