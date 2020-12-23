const readXlsxFile = require('read-excel-file/node');
import * as _ from 'lodash';
import * as fse from 'fs-extra';
import * as moment from 'moment';

let i = 0;

// const all = new ConnectionInit();

// const db = all.dbSqlite3(`D:\\MarIT\\mem\\db\\dev.db`);


// const get = (query: string) => new Promise<any[]>((res, rej) => db.all(query, (e, r) => { res(r); if (e) rej(e) }))

async function main() {
    let content = '';

    content += '-----------------------------\t\r'

    const path = `${__dirname}/excels/themes.xlsx`;
    const createFiles = fse.createWriteStream(`${__dirname}/excels/text.json`, { flags: 'w' /*flags: 'a' preserved old data*/ })

    let schema = {
        'id': { prop: 'id0', type: Number, required: false },
        "id_sous_thems": { prop: 'id', type: Number, required: false },
        // 'IdAxe': { prop: 'IdAxe', type: Number, required: false },
        'theme_fr': { prop: 'name0', type: String, required: false },
        'them_ar': { prop: 'nameAr0', type: String, required: false },
        'sous_teme_fr': { prop: 'name', type: String, required: false },
        'sous_thme_ar': { prop: 'nameAr', type: String, required: false },
        // 'Axe': { prop: 'IdAxe', type: Number, required: false },
        // 'Sous axe': { prop: 'IdSousAxe', type: Number, required: false },
        // 'Organe': { prop: 'IdOrgane', type: Number, required: false },
        // 'Procédure Spéciale': { prop: 'IdVisite', type: String, required: false },
        // 'id_pays': { prop: 'IdPays', type: Number, required: false },
        // 'id_departement': { prop: 'IdOrganisme', type: String, required: false },
    }

    interface IExcel {
        id0: number;
        id: number;
        name0: string,
        nameAr0: string,
        name: string,
        nameAr: string,

    }
    const rcms: IExcel[] = [];
    const mdl: { IdRecommendation: number, IdOrganisme: number }[] = [];

    console.log('>>>>>>>>>>>>>>>>', path)
    try {
        const sheets: { name: string }[] = await readXlsxFile(path, { getSheets: true });
        let i = 0;

        for (const s2 of sheets/*.filter((e, i) => i < 2)*/) {
            const list: { rows: IExcel[], errors: any[] } = await readXlsxFile(path, { schema, sheet: s2.name });
            if (i === 1) {
                list.rows.forEach((e, i) => {
                    // console.log(JSON.stringify(e))
                })
                createFiles.write(JSON.stringify(list.rows));
            }

            i++;
        }


        // createFiles.write('\r\n\r\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n\r\n');
        console.log('\r\n\r\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n\r\n');
        // mdl.forEach(e => {

        //     const s = `new RecomOrg { IdRecommendation = ${e.IdRecommendation}, IdOrganisme = ${e.IdOrganisme}, Date = DateTime.Now },\r\n`;

        //     createFiles.write(s);
        // })


        console.log('done');

    } catch (er) {
        const e: Error = er;
        console.error(e.name)
        console.error(e.message)
        console.error(e.stack)
    }



}

//
main();