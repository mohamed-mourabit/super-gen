import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';

const inputHtml =
    `<mat-form-field appearance="fill" class="col-md-6">
        <mat-label>{propertie}</mat-label>
        <input matInput formControlName="{propertie}" required>
    </mat-form-field>`;

const textAreaHtml =
    `<mat-form-field appearance="fill" class="col-md-12">
        <mat-label>{propertie}</mat-label>
        <textarea matInput rows="6" formControlName="{propertie}" required></textarea>
    </mat-form-field>`;

const imageHtml =
    `<div class="col-md-12">
        <app-upload-image nameBtn="Image" [folderToSaveInServer]="folderToSaveInServer" [propertyOfParent]="{propertie}To"
            [eventSubmitToParent]="{propertie}From" [eventSubmitFromParent]="eventSubmitFromParent">
        </app-upload-image>
    </div>`;

const selectHtml =
    `<mat-form-field appearance="fill" class="col-md-6">
        <mat-label>{classNav}s</mat-label>
        <mat-select formControlName="{propertie}">
            <mat-option *ngFor="let e of {classNav}s | async" [value]="e.id">{{ e.{name} }}</mat-option>
        </mat-select>
    </mat-form-field>`;

const checkBoxHtml =
    `<mat-checkbox class="col-md-6" formControlName="{propertie}"  labelPosition="before" >
        Activer
    </mat-checkbox>`;

const dateHtml =
    `<mat-form-field appearance="fill" class="col-md-6">
        <mat-label>{propertie}</mat-label>
        <input matInput [matDatepicker]="picker{i}" formControlName="{propertie}">
        <mat-datepicker-toggle matSuffix [for]="picker{i}"></mat-datepicker-toggle>
        <mat-datepicker #picker{i}></mat-datepicker>
    </mat-form-field>`;

const UPDATE_COMPONENT_HTML = 'update.component.html';
const UPDATE_COMPONENT_SCSS = 'update.component.scss';
const UPDATE_COMPONENT_TS = 'update.component.ts';

export class UpdateComponent {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }

    generateHTMLCss() {
        // else if (file === UPDATE_COMPONENT_HTML) {
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        // const distination = adminFolder;

        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${UPDATE_COMPONENT_HTML}`, 'utf8');



        // edit content
        this.configs.classes.forEach(e => {
            
            let formFields = '';
            let images = '';

            e.properties.forEach((p, i) => {

                // for section of search
                const isTypePrimitive = this.helper.isTypePrimitive(p.type);
                if (isTypePrimitive && p.name.toLowerCase() !== 'id') {

                    const isDate = p.type === 'Date';
                    const isSelect = p.name.toLowerCase() !== 'id' && p.name.startsWith('id');
                    const isCheckBox = p.type === 'boolean';
                    const isImage = p.name.includes('image');
                    const isDescription = p.name.toLowerCase().startsWith('desc');

                    if (isDate) {
                        formFields += dateHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
                        formFields = formFields.replace(/\{i\}/g, `${i}`);
                    } else if (isSelect) {
                        const { classNav, displayproperty } = this.helper.displayPropertyForSelectHtml(this.configs.classes, p.name, e);
                        formFields += selectHtml.replace(/\{classNav\}/g, classNav) + '\r\n\r\n';
                        formFields = formFields.replace(/\{propertie\}/g, p.name);

                        formFields = formFields.replace(/\{name\}/g, displayproperty);
                    } else if (isCheckBox) {
                        formFields += checkBoxHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
                    } else if (isImage) {
                        images += imageHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
                    } else if (isDescription) {
                        formFields += textAreaHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
                    } else {
                        formFields += inputHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
                    }
                }


            });
            formFields += images;

            let newContent = content.replace('{formFields}', formFields);

            const moduleName = this.helper.moduleName(this.configs.modules, e.class);
            const path = moduleName ? `${adminFolder}/${moduleName}` : adminFolder;



            // write content in new location
            fse.ensureDirSync(`${path}/${e.class}/update`);
            fse.writeFileSync(`${path}/${e.class}/update/update.component.html`, newContent);
            this.helper.progress(`>> ${e.class}/update.component.html done`);

            fse.copySync(`${this.configs.pathBaseFiles}/${UPDATE_COMPONENT_SCSS}`, `${path}/${e.class}/update/${UPDATE_COMPONENT_SCSS}`)
            this.helper.progress(`>> ${e.class}/update.component.scss done`);
        });


        // }
    }

    generateTs() {
        // else if (file === UPDATE_COMPONENT_TS) {
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        // const distination = adminFolder;

        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${UPDATE_COMPONENT_TS}`, 'utf8');
        // edit content
        this.configs.classes.forEach(e => {
            let selections = '';
            let myFormfields = '';
            let imagesInit = '';
            let imagesFrom = '';
            let imagesTo = '';

            let isThereIsImage = false;

            e.properties.forEach(p => {

                // for section of search
                const isTypePrimitive = this.helper.isTypePrimitive(p.type);
                if (isTypePrimitive) {

                    const isSelect = p.name.toLowerCase() !== 'id' && p.name.startsWith('id');
                    const isEmail = p.name.includes('email');
                    const isImage = p.name.includes('image');

                    myFormfields += `${p.name}: [this.o.${p.name}, [Validators.required, ${isEmail ? 'Validators.email' : ''}]],\r\n`;

                    if (isSelect) {
                        const { classNav, displayproperty, type } = this.helper.displayPropertyForSelectHtml(this.configs.classes, p.name, e);
                        // console.log({classNav, displayproperty, type})
                        selections += `${classNav}s = this.uow.${type}s.get();\r\n`;
                    }

                    if (isImage) {

                        imagesInit += `${p.name}To = new Subject();\r\n${p.name}From = new Subject();\r\n\r\n`;
                        imagesFrom += `this.${p.name}From.subscribe(r => this.myForm.get('${p.name}').setValue(r));\r\n`;
                        imagesTo += `this.${p.name}To.next(this.o.${p.name});;\r\n`;

                        isThereIsImage = true;
                    }
                }
            });
            // content = content.replace('/*{imports}*/', imports);
            let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
            newContent = newContent.replace(/user/g, e.class);
            newContent = newContent.replace('/*{myFormfields}*/', myFormfields);
            newContent = newContent.replace('/*{selections}*/', selections);


            if (isThereIsImage) {
                imagesTo = `setTimeout(() => { ${imagesTo}  }, 100);`;
                newContent = newContent.replace('/*{imagesInit}*/', imagesInit);
                newContent = newContent.replace('/*{imagesFrom}*/', imagesFrom);
                newContent = newContent.replace('/*{imagesTo}*/', imagesTo);
            } else {
                newContent = newContent.replace(`folderToSaveInServer = 'users';`, '');
                newContent = newContent.replace(`eventSubmitFromParent = new Subject();`, '');
                newContent = newContent.replace(/this.eventSubmitFromParent.next\(true\)\;/g, '');
            }

            const moduleName = this.helper.moduleName(this.configs.modules, e.class);
            const path = moduleName ? `${adminFolder}/${moduleName}` : adminFolder;


            // write content in new location
            fse.ensureDirSync(`${path}/${e.class}/update`);
            fse.writeFileSync(`${path}/${e.class}/update/update.component.ts`, newContent);
            this.helper.progress(`>> ${e.class}/update.component.ts done`);
        });
        // }

    }
}