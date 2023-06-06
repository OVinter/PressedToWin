import { Component } from '@angular/core';
import { SessionManagementService } from '@canonpp/duif-core';
import { SessionManagementProvider } from '@canonpp/duif-core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(sessionManagment: SessionManagementService, private readonly sessionManagementProvider: SessionManagementProvider) {


    this.sessionManagementProvider.authenticated$.subscribe(() => {
      console.log(`Access token: ${this.sessionManagementProvider.getAccessTokens(null)[0]}`);
    });

    console.log("test")
    // logs info about the currently logged in user
   /* sessionManagment.userInfo$.subscribe(userInfo => {
      console.log(userInfo);
    });*/
  }

  title = 'app';
}
