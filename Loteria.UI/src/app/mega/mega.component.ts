import { Component, OnInit } from '@angular/core';
import { ConcursoService } from './concurso.service';

@Component({
  selector: 'app-mega',
  templateUrl: './mega.component.html',
  styleUrls: ['./mega.component.css']
})
export class MegaComponent implements OnInit {

  vencedores: any;
  exibirVencedores: boolean = false;
  exibirSemVencedores: boolean = false;
  jogos: Array<any> = [];
  dezena1: Number;
  dezena2: Number;
  dezena3: Number;
  dezena4: Number;
  dezena5: Number;
  dezena6: Number;
  concursoFinalizado: boolean = false;

  constructor(private concurso: ConcursoService) { }

  ngOnInit() {
  }

  participar() {
    const dezenas = {
      dezena1: this.dezena1,
      dezena2: this.dezena2,
      dezena3: this.dezena3,
      dezena4: this.dezena4,
      dezena5: this.dezena5,
      dezena6: this.dezena6,
    }
    this.concurso
      .participar(dezenas)      
      .subscribe(
        result => {
          this.jogos.push(result.json());
          this.clear();
        },
        err => alert(err.text()));
  }

  surpresinha() {
    this.concurso
      .surpresinha()      
      .subscribe(
        result => {
          this.jogos.push(result.json());
          this.clear();
        },
        err => alert(err.text()));
  }

  sortear() {
    this.concurso
      .sortear()      
      .subscribe(
        result => { 
          this.concursoFinalizado = true;
          this.vencedores = result.json();

          if(this.vencedores.length == 0) {
            this.exibirSemVencedores = true;
          }
          else {
            this.exibirVencedores = true;
          }
        },
        err => alert(err.text()));
  }

  private clear() {
    this.dezena1 = null;
    this.dezena2 = null;
    this.dezena3 = null;
    this.dezena4 = null;
    this.dezena5 = null;
    this.dezena6 = null;
  }

}
