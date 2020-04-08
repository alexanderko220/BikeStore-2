import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { IBikeCreation } from '../admin.component';
import { IsLoadingService } from '@service-work/is-loading';

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

  //#region Params

  fileArr: any = [];
  imgArr: any = [];
  fileObj: any = [];
  form: FormGroup;
  colors: IDictionaryConainer[] = [];
  sizes: IDictionaryConainer[] = [];
  categories: IDictionaryConainer[] = [];
  subCategories: IDictionaryConainer[] = [];
  bike: IBikeCreation = null;
  title: string = null;

  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private dialogRef: MatDialogRef<AdminModalComponent>,
    private sanitizer: DomSanitizer,
    @Inject(MAT_DIALOG_DATA) private modalData: any,
    private isLoadingService: IsLoadingService) {

   
    this.form = fb.group({
      mainCategory: new FormControl(null, Validators.required),
      subCategory: new FormControl(null, Validators.required),
      model: new FormControl(null, Validators.required),
      brand: new FormControl(null, Validators.required),
      price: new FormControl(null, [Validators.required, Validators.pattern(/(^\d+([,.]\d+)?$)|((^\d{1,3}(,\d{3})+(\.\d+)?)$)|((^\d{1,3}(\.\d{3})+(,\d+)?)$)/)]),
      isInStock: new FormControl(null),
      hideRequiredControl: new FormControl(false),
      floatLabelControl: new FormControl('auto'),
      color: new FormControl(null, Validators.required),
      size: new FormControl(null, Validators.required)
    });
  }

  //#endregion Params

  //#region Helpers
  
  //#endregion Helpers

  onCategoryChange(catId: number) {
    if (catId) {

      this.bike.subCategoryId = null;

      this.http.get<IDictionaryConainer[]>("api/admin/category/" + catId)
        .subscribe(response => {
          this.subCategories = response;
          console.log(this.subCategories);
        });
    }  
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
    this.fileArr[0].isThumbImg = true;
  }

  // Clean Url
  sanitize(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  deleteAttachment(index) {
    this.fileArr.splice(index, 1)
  }

  setAsThumbImg(index) {

    for (let i = 0; i < this.fileArr.length; i++) {
      if (i === index) {
        this.fileArr[i].isThumbImg = true;
      } else {
        this.fileArr[i].isThumbImg = false;
      }
    }
  }

  async save() {
    if (this.form.valid) {
      this.dialogRef.close({ bike: this.bike, fileArr: this.fileArr});
    }
  }

  //#region Init

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
                });
    return forkJoin([r1, r2, r3]);

  }

  ngOnInit() {
    this.bike = Object.assign({}, this.modalData.data);
    this.isLoadingService.add(this.initDictionaries());
    
    if (this.modalData.isEdit) {
      this.title = 'Edit bike';
      this.onCategoryChange(this.bike.mainCategoryId);
    } else {
      this.title = 'Create bike';
    }
  }

  //#endregion Init
}
