import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactTreeComponent } from './contact-tree.component';

describe('ContactTreeComponent', () => {
  let component: ContactTreeComponent;
  let fixture: ComponentFixture<ContactTreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactTreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
