import { MapHelper } from "./map.helper";

function main(argvs: string[]) {
    const isDev = process.argv.indexOf('isdev') > -1 ? true : false;

    const m = new MapHelper(isDev);

    m.onInit();
    // m.mapAngular();
    m.mapAsp();
}

// launch programme

main(process.argv)