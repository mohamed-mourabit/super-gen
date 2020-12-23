import * as fse from 'fs-extra';
import { HelperFunctions } from '../helper.functions';
import { IConfigs } from '../map.helper';

const inputHtml =
    `<mat-form-field appearance="fill" class="col-md-6">
        <mat-label>{propertie}</mat-label>
        <input matInput [formControl]="{propertie}" required>
    </mat-form-field>`;

const selectHtml =
    `<mat-form-field appearance="fill" class="col-md-6">
        <mat-label>{classNav}s</mat-label>
        <mat-select [formControl]="{propertie}">
            <mat-option *ngFor="let e of {classNav}s | async" [value]="e.id">{{ e.{name} }}</mat-option>
        </mat-select>
    </mat-form-field>`;

const tableRow =
    `<ng-container matColumnDef="{propertieTitle}">
        <th mat-header-cell *matHeaderCellDef {mat-sort-header}>{propertieTitle2}</th>
        <td mat-cell *matCellDef="let row">{{row.{propertie}{pipe}}}</td>
    </ng-container>`;

const tableRowImage =
    `<ng-container matColumnDef="{propertieTitle}">
        <th mat-header-cell *matHeaderCellDef>{propertieTitle2}</th>
        <td mat-cell *matCellDef="let row">
            <img #img (error)="imgError(img)" [src]="displayImage(row.{propertie})" alt="" srcset="">
        </td>
    </ng-container>`;

const CLASS_COMPONENT_HTML = 'class.component.html';
const CLASS_COMPONENT_SCSS = 'class.component.scss';
const CLASS_COMPONENT_TS = 'class.component.ts';

export class ClassComponent {
    constructor(private helper: HelperFunctions, private configs: IConfigs) { }

    generateHTMLCss() {
        // else if (file === USER_COMPONENT_HTML) {
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${this.configs.currentBaseFile}`, 'utf8');

        // edit content
        this.configs.classes.forEach(e => {
            let search = '';
            let rows = '';

            e.properties.forEach(p => {

                //* for section of search
                const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                if (isTypePrimitive && p.name.toLowerCase() !== 'id' && p.type !== 'Date' && p.type !== 'boolean'
                    && !p.name.startsWith('image') && !p.name.startsWith('desc') && !p.name.includes('pass')) {
                    const isSelect = p.name.startsWith('id');
                    if (isSelect) { // generate select
                        const { classNav, displayproperty, type } = this.helper.displayPropertyForSelectHtml(this.configs.classes, p.name, e);
                        search += selectHtml.replace(/\{classNav\}/g, classNav);
                        search = search.replace('{propertie}', p.name);
                        search = search.replace('{name}', displayproperty);
                    } else { // inputs of text
                        // this condition is for specic project
                        if (p.name.toLowerCase() !== 'annee' && p.name.toLowerCase() !== 'year') {
                            search += inputHtml.replace(/\{propertie\}/g, p.name);
                        }
                    }
                }

                //* for section of table
                if (isTypePrimitive && !p.name.startsWith('desc') && !p.name.includes('pass') && p.name !== 'id' && !p.type.includes('[]')) {
                    const isPropertyNav = p.name.startsWith('id');
                    const isImage = p.name.includes('image');
                    if (isPropertyNav) {
                        const { classNav, displayproperty: property } = this.helper.displayPropertyForSelectHtml(this.configs.classes, p.name, e);
                        rows += tableRow.replace(/\{propertieTitle\}/g, classNav);
                        rows = rows.replace(/\{propertieTitle2\}/g, this.helper.toTitle(classNav));
                        // rows = rows.replace(/\{propertie\}/g, `${classNav}.${property}`);
                        rows = rows.replace(/\{propertie\}/g, `${classNav}`);
                        rows = rows.replace('{pipe}', '');
                        rows = rows.replace('{mat-sort-header}', '');
                    } else if (isImage) {
                        rows += tableRowImage.replace(/\{propertieTitle\}/g, p.name);
                        rows = rows.replace(/\{propertie\}/g, p.name);
                    } else {
                        const pipe = p.type === 'Date' ? ' | date : "dd/MM/yyyy"'
                            : (p.type === 'boolean' ? ` ? 'Oui' : 'Non'` : '');

                        rows += tableRow.replace(/\{propertieTitle\}/g, p.name);
                        rows = rows.replace(/\{propertieTitle2\}/g, this.helper.toTitle(p.name));
                        rows = rows.replace(/\{propertie\}/g, p.name);
                        rows = rows.replace('{pipe}', pipe);
                        rows = rows.replace('{mat-sort-header}', 'mat-sort-header');
                    }

                }
            });



            // let content = fse.readFileSync(`${this.configs.source}/${CLASS_COMPONENT_HTML}`, 'utf8');

            // content = content.replace('/*{imports}*/', imports);
            let newContent = content.replace(/\{model\}/g, this.helper.Cap(e.class));
            newContent = newContent.replace('{search}', search);
            newContent = newContent.replace('{tableRows}', rows);
            // if (e.properties.length >= 5) {
            //     newContent = newContent.replace('{tabsBegins}', tabsBegins);
            //     newContent = newContent.replace('{tabsEnds}', tabsEnds);
            // } else {
            //     newContent = newContent.replace('{tabsBegins}', '');
            //     newContent = newContent.replace('{tabsEnds}', '');
            // }

            const moduleName = this.helper.moduleName(this.configs.modules, e.class);
            const path = moduleName ? `${adminFolder}/${moduleName}` : adminFolder;


            // write content in new location
            fse.ensureDirSync(`${path}/${e.class}`);
            fse.writeFileSync(`${path}/${e.class}/${e.class}.component.html`, newContent);
            this.helper.progress(`>> ${e.class}.component.html done`);

            fse.copySync(`${this.configs.pathBaseFiles}/${CLASS_COMPONENT_SCSS}`, `${path}/${e.class}/${e.class}.component.scss`)
            this.helper.progress(`>> ${e.class}.component.scss done`);
        });
        // }
    }

    generateTs() {
        const adminFolder = `${this.configs.angularAppFolder}/admin`;

        let content = fse.readFileSync(`${this.configs.pathBaseFiles}/${this.configs.currentBaseFile}`, 'utf8');
        // edit content
        this.configs.classes.forEach(e => {
            let columnDefs = '';
            let formControlInit = '';
            let formControlReset = '';
            let selections = '';
            let params = '';
            let params2 = '';

            e.properties.forEach(p => {

                // for section of search
                const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                if (isTypePrimitive && p.name.toLowerCase() !== 'id' && p.type !== 'Date' && p.type !== 'boolean'
                    && !p.name.startsWith('image') && !p.name.startsWith('desc') && !p.name.includes('pass')) {

                    let value = p.type === 'string' ? '' : 0;

                    // this line is for a specifique project
                    value = p.name.toLowerCase() === 'annee' || p.name.toLowerCase() === 'year' ? new Date().getFullYear() : value;

                    formControlInit += `${p.name} = new FormControl(${value === 0 ? 0 : "''"});\r\n`;
                    formControlReset += `this.${p.name}.setValue(${value === 0 ? 0 : "''"});\r\n`;

                    params += `this.${p.name}.value === ${value === 0 ? 0 : "''"} ? ${value === 0 ? 0 : "'*'"} : this.${p.name}.value,\r\n`;
                    params2 += ` ${p.name},`;

                    const isSelect = /*p.name.toLowerCase() !== 'id' &&*/ p.name.startsWith('id');

                    if (isSelect) {
                        const { classNav, displayproperty: property, type } 
                            = this.helper.displayPropertyForSelectHtml(this.configs.classes, p.name, e);
                        selections += `${classNav}s = this.uow.${type}s.get();\r\n`;
                    }
                }

                // for section displayedColumns for table
                if (isTypePrimitive && !p.name.startsWith('desc') && !p.name.includes('pass') && p.name !== 'id' && !p.type.includes('[]')) {

                    if (p.name.startsWith('id')) {
                        const classNav = this.helper.lowerFirst(p.name.replace('id', ''));
                        columnDefs += ` '${classNav}',`;

                    } else {
                        const isImage = p.name.includes('image');
                        if (isImage) {
                            columnDefs = ` '${p.name}',` + columnDefs
                        } else {
                            columnDefs += ` '${p.name}',`;
                        }
                    }

                }
            });

            // content = content.replace('/*{imports}*/', imports);
            let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
            newContent = newContent.replace(/user/g, e.class);
            newContent = newContent.replace('/*{columnDefs}*/', columnDefs);
            newContent = newContent.replace('/*{formControlInit}*/', formControlInit);
            newContent = newContent.replace('/*{formControlReset}*/', formControlReset);
            newContent = newContent.replace(/\/\*\{params\}\*\//g, params);
            newContent = newContent.replace(/\/\*\{params2\}\*\//g, params2);
            newContent = newContent.replace('/*{params3}*/', params2);
            newContent = newContent.replace('/*{selections}*/', selections);


            const moduleName = this.helper.moduleName(this.configs.modules, e.class);
            const path = moduleName ? `${adminFolder}/${moduleName}` : adminFolder;

            // write content in new location
            fse.ensureDirSync(`${path}/${e.class}`);
            fse.writeFileSync(`${path}/${e.class}/${e.class}.component.ts`, newContent);
            this.helper.progress(`>> ${e.class}.component.ts done`);
        });

    }
}