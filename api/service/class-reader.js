// import * as ts from 'typescript';
const ts = require('typescript');
export class ClassReader {

  constructor() { }

  /**
   * 
   * @param {string} modelsTs 
   */

  methode(modelsTs) {
    const pathAbs = !process.env.IS_DEV ? `${process.cwd()}/dist` : `${process.cwd()}`
    const program = ts.createProgram([modelsTs], {
      module: ts.ModuleKind.ES2015,
      moduleResolution: ts.ModuleResolutionKind.NodeJs,
      target: ts.ScriptTarget.Latest
    });

    const typeChecker = program.getTypeChecker();
    const l = [];

    program.getSourceFiles()
      .filter(sourceFile => sourceFile.fileName.includes('models'))
      .forEach(node => {

        const statements = node.statements.filter(s => ts.isClassDeclaration(s));

        statements.forEach(statement => {
          const type = typeChecker.getTypeAtLocation(statement);
          const properties = [];
          // console.log(JSON.stringify((statement as any).name.escapedText, null, '\t'));
          const className= statement.name.escapedText;
          // console.log('-------------------------->');
          // console.log(className);
          // console.log('---');

          for (const property of type.getProperties()) {
            const propertyType = typeChecker.getTypeOfSymbolAtLocation(property, statement);
            // console.log("Name:", property.name, " | Type:", typeChecker.typeToString(propertyType));
            properties.push({ name: property.name, type: typeChecker.typeToString(propertyType) });
          }


          l.push({class: this.lowerFirst(className), properties});
        });
      });

    return l;
  }

  lowerFirst(name) {
    return name.charAt(0).toLowerCase() + name.slice(1);
  }
}
