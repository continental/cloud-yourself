import { Context } from '@azure/functions';
import * as msal from '@azure/msal-node';
import * as fancyHateoasClient from 'fancy-hateoas-client';
import { ResourceBase } from "fancy-hateoas-client";

declare var process: any;

export class Common {
    static async getRootApi(context: Context): Promise<ResourceBase> {

        // Read Config options
        const tenantId = process.env['TenantId'];
        const clientId = process.env['ClientId'];
        const clientSecret = process.env['ClientSecret'];
        const scopes = [ process.env['Scope'] ];
        const rootApiUrl = process.env['RootApiUrl'];

        const accessToken = await this.getAzureTokenViaClientCredentials(tenantId, clientId, clientSecret, scopes);

        const tokenProvider = {      
            retrieveCurrentToken: function() {
                return Promise.resolve(accessToken);
            }
        };

        const requestManager = new fancyHateoasClient.AxiosRequestManager(tokenProvider);
        const hateoasClient = new fancyHateoasClient.HateoasClient(requestManager);

        context.log('Requesting root api at ' + rootApiUrl);
        const apiRoot: ResourceBase = await hateoasClient.fetch(rootApiUrl);

        return apiRoot;
    }

    static async getAzureTokenViaClientCredentials(tenantId: string, clientId: string, clientSecret: string, scopes: string[]) {
        // Config object for msal to get token to root api
        const config = {
            auth: {
                clientId,
                authority: "https://login.microsoftonline.com/" + tenantId,
                clientSecret
            },
            system: {
                loggerOptions: {
                    loggerCallback(loglevel, message, containsPii) {
                        console.log(message);
                    },
                    piiLoggingEnabled: false,
                    logLevel: msal.LogLevel.Verbose,
                }
            }
        };
        
        // Config object for msal to get scope of api
        const clientCredentialRequest = { scopes };

        const cca = new msal.ConfidentialClientApplication(config);

        const authResponse = await cca.acquireTokenByClientCredential(clientCredentialRequest);

        return authResponse.accessToken;
    }
}