import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { IsLoadingService } from '@service-work/is-loading';
import { FileHelperService } from '../../services/fileHelper.service';
import { IDictionaryConainer, IBikeCreation, IFile } from "../../interfaces/interfaces";


@Component({
  selector: 'app-admin-modal',
  templateUrl: './admin-modal.component.html',
  styleUrls: ['./admin-modal.component.css']
})

export class AdminModalComponent implements OnInit {

  //#region Params

  fileArr: any = [];
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
    private loadingService: IsLoadingService,
    private fileService: FileHelperService
  ) {

   
    this.form = fb.group({
      mainCategory: new FormControl(null, Validators.required),
      subCategory: new FormControl(null, Validators.required),
      model: new FormControl(null, Validators.required),
      brand: new FormControl(null, Validators.required),
      price: new FormControl(null, [Validators.required, Validators.pattern(/^(\d*([.,](?=\d{3}))?\d+)+((?!\2)[.,]\d\d)?$/)]),
      isInStock: new FormControl(null),
      hideRequiredControl: new FormControl(false),
      floatLabelControl: new FormControl('auto'),
      color: new FormControl(null, Validators.required),
      size: new FormControl(null, Validators.required)
    });
  }

  //#endregion Params

  //#region Helpers

  fillFileArray(images: IFile[]) {
    for (let i = 0; i < images.length; i++) {
      const blob = this.fileService.b64ToBlob(images[i].base64String, images[i].fileType);
      const url = URL.createObjectURL(blob);
      this.fileArr.push({ item: new File([blob], images[i].fileName, { type: images[i].fileType }), url: url, isThumbImg: images[i].isThumbnail});
    }
  }

  //#endregion Helpers

  onCategoryChange(catId: number) {
    if (catId) {

      this.bike.categoryId = null;

      this.http.get<IDictionaryConainer[]>("api/admin/category/" + catId)
        .subscribe(response => {
          this.subCategories = response;
          //console.log(this.subCategories);
        });
    }  
  }

  uploadFile(e) {

    const fileListAsArray: File[] = Array.from(e);
    fileListAsArray.forEach((item, i) => {

      if (item.type.indexOf('image/') >= 0) {
        const file = (e as HTMLInputElement);
        const url = URL.createObjectURL(file[i]);
        this.fileArr.push({ item, url: url, isThumbImg: false});
      } 
      
    })
    this.fileArr[0].isThumbImg = true;
  }

  // Clean Url
  sanitize(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  deleteAttachment(index) {
    this.fileArr.splice(index, 1);
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
    this.loadingService.add(r1);

    const r2 = this.http.get<IDictionaryConainer[]>("api/admin/size")
      .subscribe(response => {
                  this.sizes =  response;
      });
    this.loadingService.add(r2);
    const r3 = this.http.get<IDictionaryConainer[]>("api/admin/category")
      .subscribe(response => {
        this.categories = response;
      });
    this.loadingService.add(r3);
    return forkJoin([r1, r2, r3]);

  }

  initSubCategory(mainId: number) {
    if (mainId) {

      const s1 = this.http.get<IDictionaryConainer[]>("api/admin/category/" + mainId)
        .subscribe(response => {
          this.subCategories = response;
        });
      this.loadingService.add(s1);
    }
  }

  initImages(storeImgId: number): Observable<any> {
    const r1 = this.http.get<IFile[]>("api/admin/img/" + storeImgId).subscribe(images => {
      if (images && images.length > 0) {
        this.fillFileArray(images);
      }
       
    });
    this.loadingService.add(r1);
    return forkJoin([r1]);
  }

  ngOnInit() {
    this.bike = Object.assign({}, this.modalData.data);
    this.initDictionaries();
    
    if (this.modalData.isEdit) {
      this.title = 'Edit bike';
      this.initSubCategory(this.bike.mainCategoryId);
      this.initImages(this.bike.imgId);
     
    } else {
      this.title = 'Create bike';
    }

  }

  //#endregion Init
}
