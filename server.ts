import 'reflect-metadata';
import { createExpressServer, useContainer, RoutingControllersOptions } from 'routing-controllers';
import { Container } from 'typedi';
import { Application } from 'express';
import * as express from 'express';

import { join } from 'path';

useContainer(Container);

class MyApp {

  constructor() {   }

  start(): Application {

    const opts: RoutingControllersOptions = {
      routePrefix: '/api',
      cors: true, //{ origin: '*', optionsSuccessStatus: 200, },
      classTransformer: true,
      controllers: [`${__dirname}/api/controllers/*.ts`, `${__dirname}/api/controllers/*.js`],
      // middlewares: [`${__dirname}/middlewares/*.ts`, `${__dirname}/middlewares/*.js`],
      // interceptors: [__dirname + '/interceptors/*.js'],
    }
    return createExpressServer(opts);
  }
}


const PORT = process.env.PORT || 3000;

new MyApp()
  // .dbConfig()
  .start()
  .use(express.static(join(__dirname, 'api/public')))
  .get('*', (req, res, next) => {
    console.log(`express:req from ${req.originalUrl}`);
    console.log(`express:req type ${req.method}`);
    next();
  })
  .listen(PORT, () => console.log(`Listening at http://localhost:${PORT}/`))
  ;


