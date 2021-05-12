import { Component, OnInit } from '@angular/core';
import { HateoasClient, ResourceBase } from 'fancy-hateoas-client';
import * as msal from '@azure/msal-browser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConfigurationService } from './core/services/configuration.service';
import { ReplaySubject, Subject } from 'rxjs';
import { shareReplay } from 'rxjs/operators';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'self-service-portal';

  userProfile: any;
  userImage: any;

  viewModel: ResourceBase | null = null;

  viewModelSubject = new ReplaySubject<ResourceBase>();
  viewModel$ = this.viewModelSubject.asObservable()

  constructor(private _msalInstance: msal.PublicClientApplication, 
              private _httpClient: HttpClient,  
              private _hateoasClient: HateoasClient,
              private _configService: ConfigurationService) {
  }

  ngOnInit(): void {
    
    this._msalInstance.handleRedirectPromise().then((tokenResponse) => {
      if(tokenResponse !== null) {
        // If the tokenResponse is not null -> we come from a token redirect and are logged in
        this._msalInstance.setActiveAccount(tokenResponse.account);
        this.fetchInitalData();
      } else {
        // If the tokenResponse is null -> no token redirect
        let accountInfos = this._msalInstance.getAllAccounts();
        if(accountInfos.length > 0) {
          // If an account is available take it to retrieve a token
          this._msalInstance.setActiveAccount(accountInfos[0]);
          this.fetchInitalData();
        } else {
          // If no account is available start login redirect
          this._msalInstance.loginRedirect({ scopes: ['user.read', this._configService.currentConfig.auth.scope] });
        }
      }
    }).catch((error) => {
        // handle error, either in the library or coming back from the server
        console.log('Error on token response: ' + error);
    });
  }

  async fetchInitalData() {
    const tokenResponse = await this._msalInstance.acquireTokenSilent( { scopes: ['user.read'] });
    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + tokenResponse.accessToken);
    // Get the user profile
    this._httpClient.get(GRAPH_ENDPOINT, { headers })
      .subscribe(profile => {
        this.userProfile = profile;
      });
    // Get the user image
    this._httpClient.get(GRAPH_ENDPOINT + '/photo/$value', { headers, responseType: 'blob' })
      .subscribe(image => {
        const reader = new FileReader();
        reader.onload = () => {
          this.userImage = reader.result;
        }
        reader.readAsDataURL(image);
      });
    console.log('Fetching root api from: ' + this._configService.currentConfig.coreApi.baseUrl);
    // Get the root view model
    this._hateoasClient.fetch(this._configService.currentConfig.coreApi.baseUrl).then(vm => {
      this.viewModel = vm;
      this.viewModelSubject.next(vm);
    });
  }

}
