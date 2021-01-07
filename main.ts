import { MapHelper } from "./api/service/map.helper";

function main(argvs: string[]) {
    const isDev = process.argv.indexOf('isdev') > -1 ? true : false;

    const m = new MapHelper(isDev);

    // m.onInit();
    // m.mapAsp();
    m.mapAngular();

    // [...Array(10).keys()].map(e => {
    //     console.log(`insert into ProductCategories (ProductsId, CategoriesId) values (${e + 1}, ${e + 1});`);
    // });
}

// launch programme

main(process.argv)