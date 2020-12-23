import { JsonController, Get, Param, Post, UploadedFiles, Res } from 'routing-controllers';
import { Request, Response } from 'express';
import * as fse from 'fs-extra';

import Container from 'typedi';
import { join } from 'path';
import { ZipFile } from '../service/zip.file';
import * as moment from 'moment';
import { GenerateAngular } from '../service/generate.angular';
import { GenerateAsp } from '../service/generate.asp';
import { HandleUserClass } from '../service/handleUserClass';
import { IConfig } from 'api/service/helper.functions';

@JsonController('/home')
export class HomeController {

  private serviceFront: GenerateAngular = Container.get(GenerateAngular);
  private serviceBack: GenerateAsp = Container.get(GenerateAsp);
  private zipService: ZipFile = Container.get(ZipFile);

  config: IConfig = {
    wholeProject: false,
    generateFolder: false,
    removeAspFolder: false,
    initFiles: false,
    addTabsInListPage: true,
  }

  // @Get('/create')
  async create() {


    const pathAbs = !process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`

    const path = `${pathAbs}/api/public`;
    // add user if not existe in models.ts

    try {
      new HandleUserClass('models.ts', path);

      let asp_folder = `${pathAbs}/api/base/asp`;

      if (this.config.removeAspFolder) {
        if (fse.pathExistsSync(`${asp_folder}`)) {
          fse.removeSync(`${asp_folder}`)
          // console.log('fse.removeSync(`${t}`)')
          // fse.mkdirSync(`${t}`, {mode: 777});
        }
      }

      // const r = await new Promise(res => setTimeout(() => res(true), 1000));
      // console.log('promise done', r)
      fse.ensureDirSync(`${asp_folder}`, { mode: 777 })

      // console.log('fse.ensureDirSync(`${t}`, {mode: 777})');

      if (this.config.wholeProject) {
        fse.copySync(`${process.cwd()}/api/services/app_source`, `${process.cwd()}/api/services/asp`);
        fse.copySync(`${pathAbs}/api/base/app_source`, `${asp_folder}`);
      }


      this.serviceFront.methode(this.config);
      this.serviceBack.methode(this.config);


      // our test folder project
      asp_folder = `${pathAbs}/generated_app2`;

      fse.pathExistsSync(`${asp_folder}/Controllers`) ? fse.removeSync(`${asp_folder}/Controllers`) : asp_folder = asp_folder;
      fse.pathExistsSync(`${asp_folder}/Migrations`) ? fse.removeSync(`${asp_folder}/Migrations`) : asp_folder = asp_folder;
      fse.pathExistsSync(`${asp_folder}/angular/src`) ? fse.removeSync(`${asp_folder}/angular/src`) : asp_folder = asp_folder;
      fse.pathExistsSync(`${asp_folder}/Models`) ? fse.removeSync(`${asp_folder}/Models`) : asp_folder = asp_folder;

      console.log('delete done')
      if (this.config.generateFolder) {
        fse.copySync(`${pathAbs}/api/base/asp`, `${pathAbs}/generated_app2`, { overwrite: true })
      }
      console.log('copy done');
      // setTimeout(() => {
      // }
      //   , 1000)
    } catch (e) {
      console.log(e)
    }

    return '';//await this.zipService.compresse();
  }

  @Get('/test')
  async test() {
    const p = new Promise(r => setInterval(() => r({ 'msg': `doen at ${moment(new Date()).format('HH:mm:ss')}` }), 500))
    return await p;
  }

  @Post("/uploadFiles/:folder")
  async uploadFiles(@Param('folder') folder: string, @UploadedFiles("files") files: Express.Multer.File[], @Res() res: Response) {
    // if (!allowedMimeTypes.includes(file.mimetype)) {
    //     throw new BadRequestError(`${file.mimetype} is not a supported file type!`);
    // }
    if (files.length === 0) {
      return res.status(500).send('File null');
    }

    const pathAbs = !process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`

    const path = `${pathAbs}/api/public`;


    fse.ensureDirSync(`${path}`);

    const date = moment(new Date()).format('DD-MM-yyyy');
    const fileName = `${date}_${files[0].originalname}`;

    new HandleUserClass(fileName, path);

    // write file uploaded in public folder
    fse.writeFileSync(`${path}/${fileName}`, files[0].buffer);

    const pathAppGenerated = `${pathAbs}/api/base/asp`;

    if (fse.pathExistsSync(pathAppGenerated)) {
      fse.removeSync(pathAppGenerated)
      fse.ensureDirSync(pathAppGenerated);
    }


    fse.copySync(`${pathAbs}/api/base/app_source`, pathAppGenerated);

    this.serviceFront.methode(this.config);
    this.serviceBack.methode(this.config);

    // fse.copySync('./asp', '../generated_app')
    const url = await this.zipService.compresse();

    return { message: 'Ok', url };
  }
}
