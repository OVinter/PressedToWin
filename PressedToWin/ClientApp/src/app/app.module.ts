import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { AppComponent } from './app.component';
import {
   SessionManagementProvider, SessionManagementService
} from '@canonpp/duif-core';
import { OidcSessionManagementConfig } from '@canonpp/common-auth';
import { OidcSessionManagementWrapper } from '@canonpp/common-auth-duif-provider';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,

  ],
  providers: [
    { provide: OidcSessionManagementConfig, useFactory: DuifAuthConfigFactory },
    { provide: SessionManagementProvider, useClass: OidcSessionManagementWrapper },
    [SessionManagementService]],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function DuifAuthConfigFactory(): OidcSessionManagementConfig {
  const ocdpLogin = 'https://login2.tst.cppinter.net/login/eu?applicationids=';
  //const ocdpLogin = 'https://login.qa.cppinter.net/login/eu?applicationids=';
  const appsId = 'c6e83503-9469-49b5-9e16-c8ab9f1e6817,08fb9ca4-661c-4659-af4c-7fecd33dd0a8,64b49a93-6331-486c-8a7b-8326e21f7ffa,';
  const appsId2 = '1746d079-6cbc-4340-8a76-e105dfd5eae7,3bdbb0de-51ef-4185-8df3-5b858b0697bb,581d0c70-8d54-496f-8708-1a50366b2960,';
  const appsId3 = '8de70da6-e2e9-451a-8b73-8d2ddf9a52e4';
  const params = '&providers=password,google,microsoft&redirect_uri=';
  return {
    loginUrl: `${ocdpLogin}${appsId}${appsId2}${appsId3}${params}` + encodeURIComponent(location.href.replace(location.hash, '')),
    logoutUrl: ''
  };
}
