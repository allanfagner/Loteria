import { MegaComponent } from './mega/mega.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ConcursoService } from './mega/concurso.service';

@NgModule({
  declarations: [
    AppComponent,
    MegaComponent
    
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule
  ],
  providers: [
    ConcursoService
  ],
  bootstrap: [MegaComponent]
})
export class AppModule { }
