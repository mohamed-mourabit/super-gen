import * as fse from 'fs-extra';

fse.ensureDirSync(`${process.cwd()}/dist/api/public`);
fse.ensureDirSync(`${process.cwd()}/dist/api/base`);

fse.copySync(`${process.cwd()}/api/public`, `${process.cwd()}/dist/api/public`);
fse.copySync(`${process.cwd()}/api/base`, `${process.cwd()}/dist/api/base`);

process.exit(0);