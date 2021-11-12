import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'apps/self-service-portal/src/environments/environment';

/** A factory function to provide an app initializer function responsible to load the configuration. */
export function configurationInitializer(configService: ConfigurationService) {
  return () => configService.loadConfiguration();
}

/** A service to get the runtime configuration of the system. */
@Injectable({ providedIn: 'root' })
export class ConfigurationService {

  // Holds the current configuration
  private _configuration: any = {};

  constructor(private _httpClient: HttpClient) { }

  /** Loads the configuration from a json file on backend. */
  loadConfiguration(): Promise<any> {
    if(environment.production) {
      return this._httpClient.get('./config/frontend.selfserviceportal.json')
                             .toPromise()
                             .then(config => this._configuration = config);
    } else {
      return this._httpClient.get('./config/frontend.selfserviceportal.dev.json')
                             .toPromise()
                             .then(config => this._configuration = config);
    }
  }

  /** Get the current configuration. */
  get currentConfig() {
    return this._configuration;
  }

}
