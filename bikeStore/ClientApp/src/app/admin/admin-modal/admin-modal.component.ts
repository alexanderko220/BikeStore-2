import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

export interface IDictionaryConainer {
  id: any,
  value: string
}

export interface IBikeCreation {
  bikeId: number,
  brand: string,
  model: string,
  isInStock: boolean,
  price: number,
  thumbImgConten: Blob,
  categoryId: number,
  storeImages: File[],
  colors: number [],
  sizes: number []
}


@Component({
  selector: 'app-admin-modal',
  templateUrl: './admin-modal.component.html',
  styleUrls: ['./admin-modal.component.css']
})

export class AdminModalComponent implements OnInit {

  fileArr: any = [];
  imgArr:  any = [];
  fileObj: any = [];
  form: FormGroup;
  colors: IDictionaryConainer[] = [];
  sizes: IDictionaryConainer[] = [];
  categories: IDictionaryConainer[] = [];
  bike: IBikeCreation = null;
  defaultBike: IBikeCreation = {
    bikeId: null,
    brand: null,
    model: null,
    isInStock: true,
    price: null,
    thumbImgConten: null,
    categoryId: null,
    storeImages: null,
    colors: [],
    sizes: []
  }

  constructor(private fb: FormBuilder, private http: HttpClient, private dialogRef: MatDialogRef<AdminModalComponent>, private sanitizer: DomSanitizer) {
    this.form = fb.group({
      category: new FormControl(null, Validators.required),
      model: new FormControl(null, Validators.required),
      brand: new FormControl(null, Validators.required),
      price: new FormControl(null, [Validators.required, Validators.pattern('^([1-9][0-9]{,2}(,[0-9]{3})*|[0-9]+)(\.[0-9]{1,9})?$')]),
      isInStock: new FormControl(null),
      hideRequiredControl: new FormControl(false),
      floatLabelControl: new FormControl('auto'),
      color: new FormControl(null, Validators.required),
      size: new FormControl(null, Validators.required),
    });
  }

  onCategoryChange() {
    console.log('works!')
  }

  uploadFile(e) {

    const fileListAsArray : File[] = Array.from(e);
    fileListAsArray.forEach((item, i) => {

      if (item.type.indexOf('image/') >= 0) {
        const file = (e as HTMLInputElement);
        const url = URL.createObjectURL(file[i]);
        this.imgArr.push(url);
        this.fileArr.push({ item, url: url, isThumbImg: false });
      } 
      
    })

    this.fileArr.forEach((item) => {
      this.fileObj.push(item.item)
    })

  }

  // Clean Url
  sanitize(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  deleteAttachment(index) {
    this.fileArr.splice(index, 1)
  }

  setAsThumbImg(index) {

  }

  save() {
    if (this.form.valid) {
      const formData = { ...this.form.value };
      formData.Files = this.fileObj;

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

    this.bike = Object.assign({}, this.defaultBike);
    this.initDictionaries();
  }

}
