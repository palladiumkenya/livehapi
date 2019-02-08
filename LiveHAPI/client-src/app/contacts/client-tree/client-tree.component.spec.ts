import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientTreeComponent } from './client-tree.component';

describe('ClientTreeComponent', () => {
  let component: ClientTreeComponent;
  let fixture: ComponentFixture<ClientTreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientTreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
