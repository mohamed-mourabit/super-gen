import * as fs from 'fs';
import { ClassReader } from './api/service2/class-reader';
import { Model } from './api/service2/generate';



export class Generate {
  primitivetypes = ['string', 'boolean', 'Date', 'number'];
  sourceFld = 'region';
  sourceFldCap = this.sourceFld.charAt(0).toUpperCase() + this.sourceFld.slice(1);

  generationFolderName = 'generation';

  tableRowExemple = `<ng-container matColumnDef="{name}">
      <th mat-header-cell *matHeaderCellDef mat-sort-header>{name}</th>
      <td mat-cell *matCellDef="let row">{{row.{name}{pipe}}}</td>
    </ng-container>`;

  formField = `<mat-form-field appearance="fill" class="col-md-6">
      <mat-label>{name}</mat-label>
      <input matInput formControlName="{name}" required>
    </mat-form-field>`;

  selectExemple = `<mat-form-field appearance="fill" class="col-md-6">
    <mat-label>{class}</mat-label>
    <mat-select formControlName="id{class}" readonly>
      <mat-option *ngFor="let e of uow.{class}s" [value]="e.id">{{ e.{name} }}</mat-option>
    </mat-select>
  </mat-form-field>`;

  checkBoxExemple = `<mat-checkbox class="col-md-6" formControlName="{name}"  labelPosition="before" >
    Activer
  </mat-checkbox>`

  constructor() { }

  methode() {

    const result = this.getAllFiles(`${this.sourceFld}`)

    // console.log(result)

    const cr = new ClassReader();

    const md = cr.methode('');

    // loop foreach class
    md.forEach(m => {

      // loop for each file
      result.forEach(r => {
        const actuelDir = `./${this.generationFolderName}/${r.path.replace(this.sourceFld, m.class.toLowerCase())}`
        const actuelFile = `./${r.name.replace(this.sourceFld, m.class.toLowerCase())}`
        const contentFile = fs.readFileSync(`${r.path}/${r.name}`, 'utf8');
        if (!fs.existsSync(actuelDir)) {
          fs.mkdirSync(actuelDir);
        }

        fs.writeFileSync(`./${actuelDir}/${actuelFile}`, this.createContent(contentFile, m));
      })

    })

    console.log('done')

  }

  createContent(oldContent: string, m: Model): string {
    if (true) {
      // console.log(oldContent)
      const className = m.class.toLowerCase();
      const classNameCap = className.charAt(0).toUpperCase() + className.slice(1);
      // return
      let newContent = oldContent.replace(new RegExp(this.sourceFld, 'g'), className);
      newContent = newContent.replace(new RegExp(this.sourceFldCap, 'gi'), classNameCap);

      let tableRowsHtml = '';
      let columnDefs = '';
      let b = '';
      let formFieldsHtml = '';
      let myFormfields = '';


      m.properties.forEach(p => {
        if (!p.name.startsWith('id')) {
          const isTypePrimitive = this.primitivetypes.indexOf(p.type) >= 0;
          
          if (p.name.includes('date')) {
            
            b = this.tableRowExemple.replace(new RegExp('{pipe}', 'g'), ' | date : "dd/MM/yyyy"');
            tableRowsHtml += b.replace(new RegExp('{name}', 'g'), p.name);
          } else {
            
            b = this.tableRowExemple.replace(new RegExp('{pipe}', 'g'), p.type.includes('bool') ? ` ? 'Oui' : 'Non'` : '');
            tableRowsHtml += b.replace(new RegExp('{name}', 'g'), p.name);
          }
          // columnDefs += JSON.stringify(p.name) + ',';
          columnDefs += `'${p.name}' ,`;

          // update
          if (!isTypePrimitive) {
            // generate a select input html 
            const cls = m.class === p.type;

            if (cls) {
              // add select input to html
              this.selectExemple = this.selectExemple.replace(new RegExp('{class}', 'g'), m.class);
              formFieldsHtml += this.selectExemple.replace(new RegExp('{name}', 'g'), m.properties[1].name);
            }
          } else if (p.type === 'boolean') {
            formFieldsHtml += this.checkBoxExemple.replace(new RegExp('{name}', 'g'), p.name) + '\r\n';
          } else {
            formFieldsHtml += this.formField.replace(new RegExp('{name}', 'g'), p.name) + '\r\n';
          }
        }
        // columnDefs += JSON.stringify(p.name) + ',';
        myFormfields += `${p.name}: [this.o.${p.name}, Validators.required],\r\n`;
      });


      // let select = '';


      // newContent = newContent.replace('{selectFields}', select);
      newContent = newContent.replace('{formFields}', formFieldsHtml);
      newContent = newContent.replace('/*{myFormfields}*/', myFormfields);

      newContent = newContent.replace('{tableRows}', tableRowsHtml);
      newContent = newContent.replace('/*{columnDefs}*/', columnDefs);


      return newContent;
    }
    return oldContent;
  }

  getAllFiles(dirPath: string, arrayOfFiles: { path: string, name: string }[] = []) {
    const self = this;
    const files = fs.readdirSync(dirPath)

    arrayOfFiles = arrayOfFiles;

    files.forEach(function (file) {
      if (fs.statSync(dirPath + "/" + file).isDirectory()) {
        arrayOfFiles = self.getAllFiles(dirPath + "/" + file, arrayOfFiles)
      } else {
        // arrayOfFiles.push(path.join(__dirname, dirPath, "/", file))
        // arrayOfFiles.push({path: path.join(__dirname, dirPath), name: file})
        arrayOfFiles.push({ path: dirPath, name: file })
      }
    })

    return arrayOfFiles
  }

  // methode() {

  //   const cr = new ClassReader();
  //   const oldName = 'region';
  //   const oldCap = oldName.charAt(0).toUpperCase() + oldName.slice(1);

  //   const dir = `./${oldName}`;
  //   let tableRow = `<ng-container matColumnDef="{name}">
  //     <th mat-header-cell *matHeaderCellDef mat-sort-header>{name}</th>
  //     <td mat-cell *matCellDef="let row">{{row.{name}{pipe}}}</td>
  //   </ng-container>`;

  //   const formField = `<mat-form-field appearance="fill" class="col-md-6">
  //     <mat-label>{name}</mat-label>
  //     <input matInput formControlName="{name}" required>
  //   </mat-form-field>`;

  //   let selectField = `<mat-form-field appearance="fill" class="col-md-6">
  //   <mat-label>{class}</mat-label>
  //   <mat-select formControlName="id{class}" readonly>
  //     <mat-option *ngFor="let e of uow.{class}s" [value]="e.id">{{ e.{name} }}</mat-option>
  //   </mat-select>
  // </mat-form-field>`;



  //   const files = fs.readdirSync(dir);
  //   const sub = fs.readdirSync(dir + '/update');



  //   const models = cr.methode();

  //   // console.log(models);



  //   models.forEach(e => {
  //     const newName = e.class.toLowerCase();
  //     const newCap = newName.charAt(0).toUpperCase() + newName.slice(1);
  //     const newdir = `./generation/${newName}`;

  //     if (!fs.existsSync(newdir)) {
  //       fs.mkdirSync(newdir);
  //     }


  //     files.forEach(file => {
  //       if (!fs.lstatSync(`${dir}/${file}`).isDirectory()) {
  //         const content = fs.readFileSync(`${dir}/${file}`, 'utf8');

  //         let newContent = content.replace(new RegExp(oldName, 'g'), newName);
  //         newContent = newContent.replace(new RegExp(oldCap, 'g'), newCap);

  //         let tableRows = '';
  //         let columnDefs = '';
  //         let b = '';

  //         e.properties.forEach(p => {
  //           if (!p.name.startsWith('id')) {
  //             if (p.name.includes('date')) {
  //               // console.log('>>>>>>>>>>>>>>>>>>>>', p.name)
  //               b = tableRow.replace(new RegExp('{pipe}', 'g'), ' | date : "dd/MM/yyyy"');
  //               tableRows += b.replace(new RegExp('{name}', 'g'), p.name);
  //             } else {
  //               // console.log('>>>>>>>>>>>>>>>>>>>>', p.name)
  //               b = tableRow.replace(new RegExp('{pipe}', 'g')
  //                 , p.type.includes('bool') ? ` ? 'Oui' : 'Non'` : '');
  //               tableRows += b.replace(new RegExp('{name}', 'g'), p.name);
  //             }
  //             // columnDefs += JSON.stringify(p.name) + ',';
  //             columnDefs += `'${p.name}' ,`;
  //           }
  //         })

  //         newContent = newContent.replace('{tableRows}', tableRows);
  //         newContent = newContent.replace('/*{columnDefs}*/', columnDefs);
  //         //
  //         // console.log(oldName, newName, oldCap, newCap)
  //         const fileName = file.replace(oldName, newName);
  //         // console.log(fileName, content.substring(0, 20))

  //         fs.writeFileSync(`${newdir}/${fileName}`, newContent);

  //       } 

  //       // fs.renameSync(filePath, newFilePath);
  //     });

  //     sub.forEach(sb => {
  //       const file = 'update';
  //         if (!fs.existsSync(`${newdir}/${file}`)) {
  //           fs.mkdirSync(`${newdir}/${file}`);
  //         }

  //         const fls = fs.readdirSync(`${dir}/${file}`);

  //         fls.forEach(f => {
  //           const content = fs.readFileSync(`${dir}/${file}/${f}`, 'utf8');

  //           let newContent = content.replace(new RegExp(oldName, 'g'), newName);
  //           newContent = newContent.replace(new RegExp(oldCap, 'g'), newCap);

  //           let formFields = '';
  //           let myFormfields = '';
  //           // let select = '';
  //           if (f.startsWith('update')) {
  //             e.properties.forEach(p => {
  //               if (!p.name.startsWith('id')) {


  //                 if (['string', 'boolean', 'Date', 'number'].indexOf(p.type) >= 0) {
  //                   formFields += formField.replace(new RegExp('{name}', 'g'), p.name) + '\r\n';
  //                 } else {
  //                   // generate a select input html 
  //                   const cls = models.find(c => c.class === p.type)

  //                   if (cls) {
  //                     // add select input to html
  //                     console.log(cls.class)

  //                     selectField = selectField.replace(new RegExp('{class}', 'g'), cls.class);
  //                     formFields += selectField.replace(new RegExp('{name}', 'g'), cls.properties[1].name);
  //                   }
  //                 }
  //               }
  //               // columnDefs += JSON.stringify(p.name) + ',';
  //               myFormfields += `${p.name}: [this.o.${p.name}, Validators.required],\r\n`;
  //             })
  //           }

  //           // newContent = newContent.replace('{selectFields}', select);
  //           newContent = newContent.replace('{formFields}', formFields);
  //           newContent = newContent.replace('/*{myFormfields}*/', myFormfields);
  //           // //
  //           // // console.log(oldName, newName, oldCap, newCap)
  //           // const fileName = file.replace(oldName, newName);
  //           // // console.log(fileName, content.substring(0, 20))

  //           fs.writeFileSync(`${newdir}/update/${f}`, newContent);
  //         })



  //     })

  //   });

  //   console.log('done')

  // }
}

