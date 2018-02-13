import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MegaComponent } from './mega.component';

describe('MegaComponent', () => {
  let component: MegaComponent;
  let fixture: ComponentFixture<MegaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MegaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MegaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
