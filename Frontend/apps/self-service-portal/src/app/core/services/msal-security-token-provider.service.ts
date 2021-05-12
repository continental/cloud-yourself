import { Injectable } from '@angular/core';
import { PublicClientApplication } from '@azure/msal-browser';
import { SecurityTokenProvider } from 'fancy-hateoas-client';
import { ConfigurationService } from './configuration.service';

@Injectable()
export class MsalSecurityTokenProviderService extends SecurityTokenProvider {

  constructor(private _msalInstance: PublicClientApplication, private _configService: ConfigurationService) { 
    super();
  }

  async retrieveCurrentToken(): Promise<string> {
    let tokenResponse = await this._msalInstance.acquireTokenSilent({ scopes: [ this._configService.currentConfig.auth.scope ] }); 
    return tokenResponse.accessToken;
  }

}
