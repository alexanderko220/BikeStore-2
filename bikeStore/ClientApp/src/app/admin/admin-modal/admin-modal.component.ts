import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef } from '@angular/material';

export interface IDictionaryConainer {
  id: any,
  value: string
}

@Component({
  selector: 'app-admin-modal',
  templateUrl: './admin-modal.component.html',
  styleUrls: ['./admin-modal.component.css']
})

export class AdminModalComponent implements OnInit {
  form: FormGroup;
  //hideRequiredControl = new FormControl(false);
  //floatLabelControl = new FormControl('auto');
  colors: IDictionaryConainer[] = [];
  sizes: IDictionaryConainer[] = [];
  categories: IDictionaryConainer[] = [];


  constructor(private fb: FormBuilder,
              private http: HttpClient,
              private dialogRef: MatDialogRef<AdminModalComponent>) {
    this.form = fb.group({
      category: new FormControl(null,Validators.required),
      model: new FormControl(null,Validators.required),
      brand: new FormControl(null,Validators.required),
      price: new FormControl(null,[Validators.required, Validators.pattern('^([1-9][0-9]{,2}(,[0-9]{3})*|[0-9]+)(\.[0-9]{1,9})?$')]),
      isInStock: new FormControl(null),
      hideRequiredControl: new FormControl(false),
      floatLabelControl: new FormControl('auto')
    });
  }

  onCategoryChange() {
    console.log('works!')
  }

  save() {
    if (this.form.valid) {
      const formData = { ...this.form.value };
      //console.log(formData)
      this.dialogRef.close(formData);
    }
   
  }

  initDictionaries(): Observable<any> {
    const r1 = this.http.get<IDictionaryConainer[]>("api/admin/color")
      .subscribe(response => {
                   this.colors = response;
                });
    const r2 = this.http.get<IDictionaryConainer[]>("api/admin/size")
      .subscribe(response => {
                  this.sizes =  response;
                });
    const r3 = this.http.get<IDictionaryConainer[]>("api/admin/category")
      .subscribe(response => {
        this.categories = response;
        console.log(this.categories);
                });
    return forkJoin([r1, r2, r3]);

  }


  ngOnInit() {
    //new FormGroup({});

    this.initDictionaries();
  }

}
