import * as fse from 'fs-extra';
import { ClassReader } from './class-reader';
import { HelperFunctions, Model, IConfig } from './helper.functions';
import Container from 'typedi';

const ADMIN_ROUTING_MODULE_TS = 'admin-routing.module.ts';
const ADMIN_MODULE_TS = 'admin.module.ts';
const ADMIN_COMPONENT_HTML = 'admin.component.html';
const UOW_SERVICE_TS = 'uow.service.ts';

const USER_ROUTING_MODULE_TS = 'class-routing.module.ts';
const USER_MODULE_TS = 'class.module.ts';

const USER_COMPONENT_HTML = 'class.component.html';
const USER_COMPONENT_SCSS = 'class.component.scss';
const USER_COMPONENT_TS = 'class.component.ts';

const UPDATE_COMPONENT_HTML = 'update.component.html';
const UPDATE_COMPONENT_SCSS = 'update.component.scss';
const UPDATE_COMPONENT_TS = 'update.component.ts';

const DETAIL_COMPONENT_HTML = 'detail.component.html';
const DETAIL_COMPONENT_SCSS = 'detail.component.scss';
const DETAIL_COMPONENT_TS = 'detail.component.ts';

const USER_SERVICE_TS = 'class.service.ts';

export class GenerateAngular {

    private helper: HelperFunctions = Container.get(HelperFunctions);
    
    constructor() { }

    methode(config: IConfig, MODELS_TS = 'models.ts') {
        const pathAbs = !process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`
        // const primitivetypes = ['string', 'boolean', 'Date', 'number'];
        const source = `${pathAbs}/api/base/source/angular`;
        const asp = `${pathAbs}/api/base/asp`;
        const admin_folder = 'admin';
        const angular_app = `${asp}/angular/src/app`;
        const files = fse.readdirSync(`${source}`);

        const classes: Model[] = new ClassReader().methode(MODELS_TS); 
        // return;
        files.forEach(file => {

            if (file === ADMIN_ROUTING_MODULE_TS) {
                // get content
                let content = fse.readFileSync(`${source}/${ADMIN_ROUTING_MODULE_TS}`, 'utf8');
                let contentHtml = fse.readFileSync(`${source}/${ADMIN_COMPONENT_HTML}`, 'utf8');
                // let imports = '';
                let routes = '';
                let navs = '';
                let navs2 = '';
                let menus =
                    `<mat-list-item [routerLink]="['/${admin_folder}/{class}']" routerLinkActive="router-active">
                        <span>{Class}s</span>
                        <mat-divider></mat-divider>
                    </mat-list-item>\r\n`;
                // edit content
                classes.forEach(e => {
                    // for ADMIN_ROUTING_MODULE_TS
                    routes += `{ path: '${e.class}', loadChildren: () => import('./${e.class}/${e.class}.module').then(m => m.${this.helper.Cap(e.class)}Module), data: {animation: '${e.class}'} },\r\n`;

                    // for ADMIN_COMPONENT_HTML
                    if (e.class.includes('user') || this.helper.propertyPrimitiveLenght(e) <= 4) {
                        // console.log(`>>>>>>>>>>>>>>nav 2 ${e.class} / ${this.helper.propertyPrimitiveLenght(e)}`);
                        navs2 += menus.replace(/\{class\}/g, e.class);
                        navs2 = navs2.replace(/\{Class\}/g, this.helper.Cap(e.class) );
                    } else {
                        // console.log(`<<<<<<<<<<<<<<< nav 1 ${e.class} / ${this.helper.propertyPrimitiveLenght(e)}`);
                        navs += menus.replace(/\{class\}/g, e.class);
                        navs = navs.replace(/\{Class\}/g, this.helper.Cap(e.class) );
                    }

                });


                content = content.replace('/*{routes}*/', routes);
                contentHtml = contentHtml.replace('{navs}', navs);
                contentHtml = contentHtml.replace('{navs2}', navs2);
                // write content in new location
                fse.ensureDirSync(`${angular_app}/${admin_folder}`);
                fse.writeFileSync(`${angular_app}/${admin_folder}/${ADMIN_ROUTING_MODULE_TS}`, content);
                fse.writeFileSync(`${angular_app}/${admin_folder}/${ADMIN_COMPONENT_HTML}`, contentHtml);

                this.helper.progress(`>> ${ADMIN_ROUTING_MODULE_TS} done`);
                this.helper.progress(`>> ${ADMIN_COMPONENT_HTML} done`);
                
                if (config.initFiles) {
                    fse.copySync(`${source}/${ADMIN_MODULE_TS}`, `${angular_app}/${admin_folder}/${ADMIN_MODULE_TS}`);
                    this.helper.progress(`>> ${ADMIN_MODULE_TS} done`);
                }

                fse.copySync(`${pathAbs}/api/public/${MODELS_TS}`, `${angular_app}/models/${MODELS_TS}`);
                this.helper.progress(`>> ${MODELS_TS} done`);
            }

            else if (file === UOW_SERVICE_TS) { // and services
                const distination = `${asp}/angular/src/app/services`;
                fse.ensureDirSync(distination);

                let content = fse.readFileSync(`${source}/${UOW_SERVICE_TS}`, 'utf8');
                let contentService = fse.readFileSync(`${source}/${USER_SERVICE_TS}`, 'utf8');

                let imports = '';
                let services = '';
                // edit content
                classes.forEach(e => {
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
            }

            // angular
            else if (file === USER_COMPONENT_HTML) {
                const distination = `${asp}/angular/src/app/${admin_folder}`;

                let content = fse.readFileSync(`${source}/${USER_COMPONENT_HTML}`, 'utf8');

                let inputHtml =
                `<mat-form-field appearance="fill" class="col-md-6">
                    <mat-label>{propertie}</mat-label>
                    <input matInput [formControl]="{propertie}" required>
                </mat-form-field>`;

                let selectHtml =
                `<mat-form-field appearance="fill" class="col-md-6">
                    <mat-label>{classNav}s</mat-label>
                    <mat-select [formControl]="{propertie}">
                        <mat-option *ngFor="let e of {classNav}s | async" [value]="e.id">{{ e.{name} }}</mat-option>
                    </mat-select>
                </mat-form-field>`;

                let tableRow =
                `<ng-container matColumnDef="{propertieTitle}">
                    <th mat-header-cell *matHeaderCellDef {mat-sort-header}>{propertieTitle2}</th>
                    <td mat-cell *matCellDef="let row">{{row.{propertie}{pipe}}}</td>
                </ng-container>`;

                let tableRowImage =
                    `<ng-container matColumnDef="{propertieTitle}">
                    <th mat-header-cell *matHeaderCellDef>{propertieTitle2}</th>
                    <td mat-cell *matCellDef="let row">
                        <img #img (error)="imgError(img)" [src]="displayImage(row.{propertie})" alt="" srcset="">
                    </td>
                </ng-container>`;

                let tabsBegins = 
                `<mat-tab-group class="mat-elevation-z4 mt-3" (selectedIndexChange)="selectedIndexChange($event)">
                    <mat-tab label="Liste">
                    <ng-template mat-tab-label>
                        <mat-icon class="example-tab-icon">view_list</mat-icon> Liste
                    </ng-template>
                    `;
                let tabsEnds = 
                    `</mat-tab>
                    <mat-tab label="Graphe">
                    <ng-template mat-tab-label>
                        <mat-icon class="example-tab-icon">assessment</mat-icon> Graphe
                    </ng-template>
                      <div class="mat-elevation-z8 m-2">
                        <app-dynamic-chart [dataSubject]="dataSubject"></app-dynamic-chart>
                      </div>
                    </mat-tab>
                    <mat-tab label="Synthèse Central">
                        <ng-template mat-tab-label>
                            <mat-icon class="example-tab-icon">note</mat-icon> Synthèse Regional
                        </ng-template>
                        <div class="m-2">
                            <app-syntheseRegional [moduleName]="breadcrumb.name" ></app-syntheseRegional>
                        </div>
                    </mat-tab>
                  </mat-tab-group>`;

                // edit content
                classes.forEach(e => {
                    fse.ensureDirSync(`${distination}/${e.class}`);
                    let search = '';
                    let rows = '';

                    e.properties.forEach(p => {

                        //* for section of search
                        const isTypePrimitive = this.helper.isTypePrimitive(p.type);

                        if (isTypePrimitive && p.name.toLowerCase() !== 'id' && p.type !== 'Date' && p.type !== 'boolean'
                            && !p.name.startsWith('image') && !p.name.startsWith('desc') && !p.name.includes('pass')) {
                            const isSelect = p.name.startsWith('id');
                            if (isSelect) { // generate select
                                const { classNav, displayproperty, type } = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
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
                                const { classNav, displayproperty: property } = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
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

                    // content = content.replace('/*{imports}*/', imports);
                    let newContent = content.replace(/\{model\}/g, this.helper.Cap(e.class));
                    newContent = newContent.replace('{search}', search);
                    newContent = newContent.replace('{tableRows}', rows);
                    if (e.properties.length >= 5) {
                        newContent = newContent.replace('{tabsBegins}', tabsBegins);
                        newContent = newContent.replace('{tabsEnds}', tabsEnds);
                    } else {
                        newContent = newContent.replace('{tabsBegins}', '');
                        newContent = newContent.replace('{tabsEnds}', '');
                    }
                    

                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}`);
                    fse.writeFileSync(`${distination}/${e.class}/${e.class}.component.html`, newContent);
                    this.helper.progress(`>> ${e.class}.component.html done`);

                    fse.copySync(`${source}/${USER_COMPONENT_SCSS}`, `${distination}/${e.class}/${e.class}.component.scss`)
                    this.helper.progress(`>> ${e.class}.component.scss done`);
                });
            }


            else if (file === UPDATE_COMPONENT_TS) {
                const distination = `${asp}/angular/src/app/${admin_folder}`;

                let content = fse.readFileSync(`${source}/${UPDATE_COMPONENT_TS}`, 'utf8');
                // edit content
                classes.forEach(e => {
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
                                const {classNav, displayproperty, type} = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
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


                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}/update`);
                    fse.writeFileSync(`${distination}/${e.class}/update/update.component.ts`, newContent);
                    this.helper.progress(`>> ${e.class}/update.component.ts done`);
                });
            }


            else if (file === UPDATE_COMPONENT_HTML) {
                const distination = `${asp}/angular/src/app/${admin_folder}`;

                let content = fse.readFileSync(`${source}/${UPDATE_COMPONENT_HTML}`, 'utf8');

                let inputHtml =
                    `<mat-form-field appearance="fill" class="col-md-6">
                    <mat-label>{propertie}</mat-label>
                    <input matInput formControlName="{propertie}" required>
                </mat-form-field>`;

                let textAreaHtml =
                `<mat-form-field appearance="fill" class="col-md-12">
                    <mat-label>{propertie}</mat-label>
                    <textarea matInput rows="6" formControlName="{propertie}" required></textarea>
                </mat-form-field>`

                let imageHtml =
                `<div class="col-md-12">
                    <app-upload-image nameBtn="Image" [folderToSaveInServer]="folderToSaveInServer" [propertyOfParent]="{propertie}To"
                        [eventSubmitToParent]="{propertie}From" [eventSubmitFromParent]="eventSubmitFromParent">
                    </app-upload-image>
                </div>`;

                let selectHtml =
                    `<mat-form-field appearance="fill" class="col-md-6">
                    <mat-label>{classNav}s</mat-label>
                    <mat-select formControlName="{propertie}">
                        <mat-option *ngFor="let e of {classNav}s | async" [value]="e.id">{{ e.{name} }}</mat-option>
                    </mat-select>
                </mat-form-field>`;

                let checkBoxHtml =
                    `<mat-checkbox class="col-md-6" formControlName="{propertie}"  labelPosition="before" >
                    Activer
                </mat-checkbox>`;

                let dateHtml =
                    `<mat-form-field appearance="fill" class="col-md-6">
                    <mat-label>{propertie}</mat-label>
                    <input matInput [matDatepicker]="picker{i}" formControlName="{propertie}">
                    <mat-datepicker-toggle matSuffix [for]="picker{i}"></mat-datepicker-toggle>
                    <mat-datepicker #picker{i}></mat-datepicker>
                </mat-form-field>`;

                // edit content
                classes.forEach(e => {
                    fse.ensureDirSync(`${distination}/${e.class}`);
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
                                const { classNav, displayproperty } = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
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


                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}/update`);
                    fse.writeFileSync(`${distination}/${e.class}/update/update.component.html`, newContent);
                    this.helper.progress(`>> ${e.class}/update.component.html done`);

                    fse.copySync(`${source}/${UPDATE_COMPONENT_SCSS}`, `${distination}/${e.class}/update/${UPDATE_COMPONENT_SCSS}`)
                    this.helper.progress(`>> ${e.class}/update.component.scss done`);
                });


            }

            // else if (file === DETAIL_COMPONENT_TS) {
            //     const distination = `${asp}/angular/src/app/${admin_folder}`;

            //     let content = fse.readFileSync(`${source}/${DETAIL_COMPONENT_TS}`, 'utf8');
            //     // edit content
            //     classes.forEach(e => {
            //         let selections = '';
            //         let myFormfields = '';
            //         let imagesInit = '';
            //         let imagesFrom = '';
            //         let imagesTo = '';

            //         let isThereIsImage = false;

            //         e.properties.forEach(p => {

            //             // for section of search
            //             const isTypePrimitive = this.helper.isTypePrimitive(p.type);
            //             if (isTypePrimitive) {

            //                 const isSelect = p.name.toLowerCase() !== 'id' && p.name.startsWith('id');
            //                 const isEmail = p.name.includes('email');
            //                 const isImage = p.name.includes('image');

            //                 myFormfields += `${p.name}: [this.o.${p.name}, [Validators.required, ${isEmail ? 'Validators.email' : ''}]],\r\n`;

            //                 if (isSelect) {
            //                     const {classNav, displayproperty, type} = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
            //                     // console.log({classNav, displayproperty, type})
            //                     selections += `${classNav}s = this.uow.${type}s.get();\r\n`;
            //                 }

            //                 if (isImage) {

            //                     imagesInit += `${p.name}To = new Subject();\r\n${p.name}From = new Subject();\r\n\r\n`;
            //                     imagesFrom += `this.${p.name}From.subscribe(r => this.myForm.get('${p.name}').setValue(r));\r\n`;
            //                     imagesTo += `this.${p.name}To.next(this.o.${p.name});;\r\n`;

            //                     isThereIsImage = true;
            //                 }
            //             }
            //         });
            //         // content = content.replace('/*{imports}*/', imports);
            //         let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
            //         newContent = newContent.replace(/user/g, e.class);
            //         newContent = newContent.replace('/*{myFormfields}*/', myFormfields);
            //         newContent = newContent.replace('/*{selections}*/', selections);
                    

            //         if (isThereIsImage) {
            //             imagesTo = `setTimeout(() => { ${imagesTo}  }, 100);`;
            //             newContent = newContent.replace('/*{imagesInit}*/', imagesInit);
            //             newContent = newContent.replace('/*{imagesFrom}*/', imagesFrom);
            //             newContent = newContent.replace('/*{imagesTo}*/', imagesTo);
            //         } else {
            //             newContent = newContent.replace(`folderToSaveInServer = 'users';`, '');
            //             newContent = newContent.replace(`eventSubmitFromParent = new Subject();`, '');
            //             newContent = newContent.replace(/this.eventSubmitFromParent.next\(true\)\;/g, '');
            //         }


            //         // write content in new location
            //         fse.ensureDirSync(`${distination}/${e.class}/detail`);
            //         fse.writeFileSync(`${distination}/${e.class}/detail/${DETAIL_COMPONENT_TS}`, newContent);
            //         this.helper.progress(`>> ${e.class}/detail/${DETAIL_COMPONENT_TS} done`);
            //     });
            // }


            // else if (file === DETAIL_COMPONENT_HTML) {
            //     const distination = `${asp}/angular/src/app/${admin_folder}`;

            //     let content = fse.readFileSync(`${source}/${DETAIL_COMPONENT_HTML}`, 'utf8');

            //     let inputHtml =
            //         `<mat-form-field appearance="fill" class="col-md-6">
            //         <mat-label>{propertie}</mat-label>
            //         <input matInput [disabled]="true" formControlName="{propertie}" required>
            //     </mat-form-field>`;

            //     let textAreaHtml =
            //     `<mat-form-field appearance="fill" class="col-md-12">
            //         <mat-label>{propertie}</mat-label>
            //         <textarea matInput [disabled]="true" rows="6" formControlName="{propertie}" required></textarea>
            //     </mat-form-field>`

            //     let imageHtml =
            //     `<div class="col-md-12">
            //         <app-upload-image nameBtn="Image" [folderToSaveInServer]="folderToSaveInServer" [propertyOfParent]="{propertie}To"
            //             [eventSubmitToParent]="{propertie}From" [eventSubmitFromParent]="eventSubmitFromParent">
            //         </app-upload-image>
            //     </div>`;

            //     let selectHtml =
            //         `<mat-form-field appearance="fill" class="col-md-6">
            //         <mat-label>{classNav}s</mat-label>
            //         <mat-select readonly formControlName="{propertie}">
            //             <mat-option *ngFor="let e of {classNav}s | async" [value]="e.id">{{ e.{name} }}</mat-option>
            //         </mat-select>
            //     </mat-form-field>`;

            //     let checkBoxHtml =
            //         `<mat-checkbox [disabled]="true" class="col-md-6" formControlName="{propertie}"  labelPosition="before" >
            //         Activer
            //     </mat-checkbox>`;

            //     let dateHtml =
            //         `<mat-form-field appearance="fill" class="col-md-6">
            //         <mat-label>{propertie}</mat-label>
            //         <input matInput [matDatepicker]="picker{i}" formControlName="{propertie}">
            //         <mat-datepicker-toggle matSuffix [for]="picker{i}"></mat-datepicker-toggle>
            //         <mat-datepicker #picker{i}></mat-datepicker>
            //     </mat-form-field>`;

            //     // edit content
            //     classes.forEach(e => {
            //         fse.ensureDirSync(`${distination}/${e.class}`);
            //         let formFields = '';
            //         let images = '';

            //         e.properties.forEach((p, i) => {

            //             // for section of search
            //             const isTypePrimitive = this.helper.isTypePrimitive(p.type);
            //             if (isTypePrimitive && p.name.toLowerCase() !== 'id') {

            //                 const isDate = p.type === 'Date';
            //                 const isSelect = p.name.toLowerCase() !== 'id' && p.name.startsWith('id');
            //                 const isCheckBox = p.type === 'boolean';
            //                 const isImage = p.name.includes('image');
            //                 const isDescription = p.name.toLowerCase().startsWith('desc');

            //                 if (isDate) {
            //                     formFields += dateHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
            //                     formFields = formFields.replace(/\{i\}/g, `${i}`);
            //                 } else if (isSelect) {
            //                     const { classNav, displayproperty } = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
            //                     formFields += selectHtml.replace(/\{classNav\}/g, classNav) + '\r\n\r\n';
            //                     formFields = formFields.replace(/\{propertie\}/g, p.name);

            //                     formFields = formFields.replace(/\{name\}/g, displayproperty);
            //                 } else if (isCheckBox) {
            //                     formFields += checkBoxHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
            //                 } else if (isImage) {
            //                     images += imageHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
            //                 } else if (isDescription) {
            //                     formFields += textAreaHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
            //                 } else {
            //                     formFields += inputHtml.replace(/\{propertie\}/g, p.name) + '\r\n\r\n';
            //                 }
            //             }

                        
            //         });
            //         formFields += images;

            //         let newContent = content.replace('{formFields}', formFields);


            //         // write content in new location
            //         fse.ensureDirSync(`${distination}/${e.class}/detail`);
            //         fse.writeFileSync(`${distination}/${e.class}/detail/${DETAIL_COMPONENT_HTML}`, newContent);
            //         this.helper.progress(`>> ${e.class}/detail/${DETAIL_COMPONENT_HTML} done`);

            //         fse.copySync(`${source}/${DETAIL_COMPONENT_SCSS}`, `${distination}/${e.class}/detail/${DETAIL_COMPONENT_SCSS}`)
            //         this.helper.progress(`>> ${e.class}/detail/${DETAIL_COMPONENT_SCSS} done`);
            //     });


            // }


            else if (file === USER_COMPONENT_TS) {

                const distination = `${asp}/angular/src/app/${admin_folder}`;

                let content = fse.readFileSync(`${source}/${USER_COMPONENT_TS}`, 'utf8');
                // edit content
                classes.forEach(e => {
                    fse.ensureDirSync(`${distination}/${e.class}`);
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
                            value = p.name.toLowerCase() === 'annee' || p.name.toLowerCase() === 'year'? new Date().getFullYear() : value;

                            formControlInit += `${p.name} = new FormControl(${value === 0 ? 0 : "''"});\r\n`;
                            formControlReset += `this.${p.name}.setValue(${value === 0 ? 0 : "''"});\r\n`;

                            params += `this.${p.name}.value === ${value === 0 ? 0 : "''"} ? ${value === 0 ? 0 : "'*'"} : this.${p.name}.value,\r\n`;
                            params2 += ` ${p.name},`;

                            const isSelect = /*p.name.toLowerCase() !== 'id' &&*/ p.name.startsWith('id');

                            if (isSelect) {
                                const {classNav, displayproperty: property, type} = this.helper.displayPropertyForSelectHtml(classes, p.name, e);
                                selections += `${classNav}s = this.uow.${type}s.get();\r\n`;
                            }
                        }

                        // for section displayedColumns for table
                        if (isTypePrimitive && !p.name.startsWith('desc') && !p.name.includes('pass') && p.name !== 'id' && !p.type.includes('[]')) {

                            if (p.name.startsWith('id')) {
                                const classNav = this.helper.lowerFirst(p.name.replace('id', '')) ;
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

                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}`);
                    fse.writeFileSync(`${distination}/${e.class}/${e.class}.component.ts`, newContent);
                    this.helper.progress(`>> ${e.class}.component.ts done`);
                });

            }


            else if (file === USER_ROUTING_MODULE_TS) {
                // get content
                const distination = `${asp}/angular/src/app/${admin_folder}`;
                let content = fse.readFileSync(`${source}/${USER_ROUTING_MODULE_TS}`, 'utf8');

                // edit content
                classes.forEach(e => {

                    let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
                    newContent = newContent.replace(/user/g, e.class);

                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}`);
                    fse.writeFileSync(`${distination}/${e.class}/${e.class}-routing.module.ts`, newContent);
                    this.helper.progress(`>> ${e.class}-routing.module.ts done`);
                });
            }


            else if (file === USER_MODULE_TS) {
                // get content
                const distination = `${asp}/angular/src/app/${admin_folder}`;
                let content = fse.readFileSync(`${source}/${USER_MODULE_TS}`, 'utf8');

                // edit content
                classes.forEach(e => {

                    let newContent = content.replace(/User\$/g, this.helper.Cap(e.class));
                    newContent = newContent.replace(/user/g, e.class);

                    // write content in new location
                    fse.ensureDirSync(`${distination}/${e.class}`);
                    fse.writeFileSync(`${distination}/${e.class}/${e.class}.module.ts`, newContent);
                    this.helper.progress(`>> ${e.class}.module.ts done`);
                });
            }

        })
    }

}