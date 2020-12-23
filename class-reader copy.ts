import * as ts from 'typescript';

export class ClassReader {

  constructor() { }

  methode(MODELS_TS: string) {
    const program = ts.createProgram([`${process.cwd()}/api/public/${MODELS_TS}`], {
      module: ts.ModuleKind.ES2015,
      moduleResolution: ts.ModuleResolutionKind.NodeJs,
      target: ts.ScriptTarget.Latest
    });

    const typeChecker = program.getTypeChecker();
    const l: { class: string, properties: { name: string, type: string }[] }[] = [];

    program.getSourceFiles()
      .filter(sourceFile => sourceFile.fileName.includes('models'))
      .forEach(node => {

        const statements = node.statements.filter(s => ts.isClassDeclaration(s));

        statements.forEach(statement => {
          const type = typeChecker.getTypeAtLocation(statement);
          const properties = [];
          // console.log(JSON.stringify((statement as any).name.escapedText, null, '\t'));
          const className: string = (statement as any).name.escapedText;
          // console.log('-------------------------->');
          // console.log(className);
          // console.log('---');

          for (const property of type.getProperties()) {
            const propertyType = typeChecker.getTypeOfSymbolAtLocation(property, statement);
            // console.log("Name:", property.name, " | Type:", typeChecker.typeToString(propertyType));
            properties.push({ name: property.name, type: typeChecker.typeToString(propertyType) });
          }


          l.push({class: className.toLowerCase(), properties});
        });
      });

    return l;
  }
}
