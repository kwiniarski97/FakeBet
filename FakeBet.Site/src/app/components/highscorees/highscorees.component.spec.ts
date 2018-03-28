import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HighscoreesComponent } from './highscorees.component';

describe('HighscoreesComponent', () => {
  let component: HighscoreesComponent;
  let fixture: ComponentFixture<HighscoreesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HighscoreesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HighscoreesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
