import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimarytreeListComponent } from './primarytree-list.component';

describe('PrimarytreeListComponent', () => {
  let component: PrimarytreeListComponent;
  let fixture: ComponentFixture<PrimarytreeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimarytreeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimarytreeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
