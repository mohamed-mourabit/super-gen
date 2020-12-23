import * as zipper from 'zip-local';
import * as moment from 'moment';


export class ZipFile {

    compresse(
        sourceFolder: string = `${!process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`}/api/base/asp` 
    , distination: string = `${!process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`}/api/public`) {
        const date = moment(new Date()).format('DD-MM-yyyy');
        const fileName = `project_${date}.zip`;

        return new Promise(resolve => {
            zipper.sync.zip(sourceFolder).compress().save(`${distination}/${fileName}`);
            resolve(fileName);
        });
    }
}