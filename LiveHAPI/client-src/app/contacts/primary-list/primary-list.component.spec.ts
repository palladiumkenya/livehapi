import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimaryListComponent } from './primary-list.component';

describe('PrimaryListComponent', () => {
  let component: PrimaryListComponent;
  let fixture: ComponentFixture<PrimaryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimaryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimaryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
