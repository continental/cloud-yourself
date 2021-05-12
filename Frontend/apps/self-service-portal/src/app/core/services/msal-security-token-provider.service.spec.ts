/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MsalSecurityTokenProviderService } from './msal-security-token-provider.service';

describe('Service: MsalSecurityTokenProvider', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MsalSecurityTokenProviderService]
    });
  });

  it('should ...', inject([MsalSecurityTokenProviderService], (service: MsalSecurityTokenProviderService) => {
    expect(service).toBeTruthy();
  }));
});
