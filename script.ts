import * as path from 'path';
import * as readline from 'readline';
import * as fs from 'fs';
import * as fse from 'fs-extra';
import * as url from 'url';

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
  terminal: false
});

const prompt = (question) => new Promise(res => rl.question(question, r => res(r)))

// g.methode();
// console.log(process.env.npm_package_args_p1_p2)

// function myFirstCallBack(x: number, y: number, callback: (s: number) => void) {
//   return callback(x+y);
// }

// myFirstCallBack(5, 6, (e) => {
//   console.log(e);
// })

const convert = (from, to) => str => Buffer.from(str, from).toString(to)
// const utf8ToHex = convert('utf8', 'hex')
// const hexToUtf8 = convert('hex', 'utf8')


// console.log(utf8ToHex('EL'));

const test = (state) => ({ do: () => console.log(state) });

// (async () => await new Promise(r => r()))();

// test('me & you').do();

//   let e = f.replace(/\./g, '_');
//   e = e.replace(/\-/g, '_');

// String.prototype.capitalize = function() {
//   return this.charAt(0).toUpperCase() + this.slice(1)
// }

import { HomeController } from './api/controllers/home.controller';


new HomeController().create().then(r => console.log('anytime ser : ', r));
// console.log('anytime ser : ', r);
process.exit(0);