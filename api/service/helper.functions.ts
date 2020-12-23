export interface Model {
    class: string;
    properties: { name: string; type: string; }[];
}

export class HelperFunctions {

    constructor() {
        console.log('>>>>>>>>>>>>>>HelperFunctions')
    }

    public moduleName(modules: { module: string; classes: string[]; }[], className: string): string {
        let module = null;
        modules.forEach(m => {
            if (m.classes.map(e => e.toLowerCase()).includes(className.toLowerCase())) {
                module = m.module;
                return;
            }
        });

        return module;
    }

    public Cap(word: string): string {
        return word.charAt(0).toUpperCase() + word.slice(1);
    }

    public displayPropertyForSelectHtml(classes: Model[], propertyNameActuel: string, clsActuel: Model) {
        const classNav = this.lowerFirst(propertyNameActuel.replace('id', ''));
        const o  = clsActuel.properties.find(e => e.name === classNav)
        // const realType = o ? o.type : classNav;
        const realType = this.searchFromPropertyToClassName(propertyNameActuel, clsActuel);

        const parentClass = classes.find(c => {
            // console.log( c.class, realType)
            return  c.class === realType;
        });
        // console.log(parentClass)
        if (parentClass) {
            return { classNav, displayproperty: parentClass.properties[1].name, type : this.lowerFirst(realType) }
        }
        // console.log('>>>>>>>>>>>>>>>>', { classNav, displayproperty: 'name', type : this.lowerFirst(realType) }) 
        return { classNav, displayproperty: 'name', type : this.lowerFirst(realType) }
    }

    toTitle(name: string) {
        name = name.charAt(0).toUpperCase() + name.slice(1);

        return name.split('').map((e, i) => i !== 0 && e.toUpperCase() === e ? ` ${e}` : `${e}`).join('')
    }

    // in the same class, aka idUser is belong to relation ships of type User
    searchFromPropertyToClassName(idClass: string, clsActuel: Model): string {
        const classNav = this.lowerFirst(idClass.replace('id', ''));

        const obj = clsActuel.properties.find(e => e.name === classNav);

        return obj ? this.lowerFirst(obj.type) : classNav;
    }

    propertyPrimitiveLenght(cls: Model): number {
        return cls.properties.filter(e => this.isTypePrimitive(e.type)).length;
    }

    spaceTab(i: number) {
        return Array(i).map(() => `\t`).join('');
    }

    isTypePrimitive(type: string) {
        const primitivetypes = ['string', 'boolean', 'Date', 'number'];

        return primitivetypes.indexOf(type) >= 0;
    }

    getNameFor_withOne_ef_relation(clsAct: string, propNav: { name: string, type: string; }, all: Model[]) {

        const clsNav = propNav.type.replace('[]', '').toLowerCase();

        const clsNavWithProp = all.find(e => e.class.toLowerCase() === clsNav.toLowerCase())

        let p: { name: string, type: string } = null;

        if (clsNavWithProp) {
            p = clsNavWithProp.properties.find(e => /*e.type === clsAct &&*/ propNav.name.toLowerCase().includes(e.name.toLowerCase()));
            return p ? p : propNav;
        }

        return propNav;
    }

    getNameFor_HasOne_ef_relation(clsAct: string, propNav: { name: string, type: string; }, all: Model[]) {

        const clsNav = propNav.type.replace('[]', '').toLowerCase();

        const clsNavWithProp = all.find(e => e.class.toLowerCase() === propNav.type.toLowerCase())

        let p: { name: string, type: string } = null;

        if (clsNavWithProp) {
            p = clsNavWithProp.properties.find(e => {

            //  console.log(clsAct.toLowerCase(), e.type.toLowerCase())
                return e.name.toLowerCase().includes(propNav.name.toLowerCase()) && e.type.toLowerCase().includes(clsAct.toLowerCase())
            });
            return p ? p : propNav;
        }

        return propNav;
    }

    lowerFirst(name: string) {
        return name.charAt(0).toLowerCase() + name.slice(1);
    }

    progress(info: string) {
        // console.log(info);
    }

    removeZoneOfText(source: string, begin: string, end: string) {
        const indexBegin = source.indexOf(begin);
        const indexEnd = source.indexOf(end);
        const textToRemove = source.substring(indexBegin, indexEnd);
        const r = source.replace(end, '');
        // console.log(textToRemove)
        // console.log('>>>>>>>>>>>>>>>>>>>>>>>>>>>')
        return r.replace(textToRemove, '');
    }


}

export interface IConfig {
    wholeProject: boolean;
    generateFolder: boolean;
    removeAspFolder: boolean;
    initFiles: boolean;
    addTabsInListPage?: boolean;
}