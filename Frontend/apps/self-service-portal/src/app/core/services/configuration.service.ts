import { Injectable } from '@angular/core';

// Glogal configuration variables included by index.html
declare var config: any;
declare var configDev: any;

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  private _configuration: any = {};

  constructor() { 
    if(configDev) {
      this._configuration = configDev;
    } else {
      this._configuration = config;
    }
  }

  get currentConfig() {
    return this._configuration;
  }
}
