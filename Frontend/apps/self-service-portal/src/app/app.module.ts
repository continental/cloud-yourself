import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';

import { AppComponent } from './app.component';
import { HateoasClientModule, SecurityTokenProvider } from 'fancy-ngx-hateoas-client';
import { PublicClientApplication } from '@azure/msal-browser';
import { MsalSecurityTokenProviderService } from './core/services/msal-security-token-provider.service';
import { ConfigurationService } from './core/services/configuration.service';

@NgModule({
  declarations: [	
    AppComponent,
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterializeUiCoreModule,
    HateoasClientModule.forRoot({ sendSecurityToken: true}),
    SharedModule,
    CoreModule
  ],
  bootstrap: [AppComponent],
  providers: [
    {
      provide: PublicClientApplication,
      deps: [ConfigurationService],
      useFactory: (configService: ConfigurationService) => new PublicClientApplication(configService.currentConfig)
    },
    {
      provide: SecurityTokenProvider,
      useClass: MsalSecurityTokenProviderService
    }
  ]
})
export class AppModule { }
