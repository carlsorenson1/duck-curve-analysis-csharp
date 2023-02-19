import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { EnergyService } from './energy.service';

import { AppComponent } from './app.component';
import { HomeComponent } from './home.component';
import { ExploreComponent } from './explore.component';
import { ConclusionsComponent } from './conclusions.component';

@NgModule({
  declarations: [
    AppComponent, HomeComponent, ExploreComponent, ConclusionsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'explore', component: ExploreComponent },
      { path: 'conclusions', component: ConclusionsComponent }
    ], { useHash: true })
  ],
  providers: [
    EnergyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
