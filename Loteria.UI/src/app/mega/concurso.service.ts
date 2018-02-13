import { Injectable } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import { MegaComponent } from './mega.component';

@Injectable()
export class ConcursoService {

  url: string = 'http://localhost:52762/api/concurso/';

  constructor(private http: Http) {
    
  }

  participar(bilhete) {
     return this.http
        .post(`${this.url}megasena/bilhete`, bilhete);
  } 

  surpresinha() {
    return this.http
        .post(`${this.url}megasena/bilhete/surpresa`, null);
  }

  sortear() {
    return this.http
       .post(`${this.url}megasena/sorteio`, null);
 } 

}
