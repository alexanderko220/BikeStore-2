import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AgGridAngular } from 'ag-grid-angular';
import { AllCommunityModules, Module } from "@ag-grid-community/all-modules";
import { MatDialog } from '@angular/material/dialog';
import { AdminModalComponent } from './admin-modal/admin-modal.component';
import { IsLoadingService } from '@service-work/is-loading';
import { ButtonRenderComponent } from '../ag-render/button-render/button-render.component';


export interface IBikeCreation {
  bikeId: number;
  brand: string;
  model: string;
  isInStock: boolean;
  price: number;
  thumbImgContent: string | ArrayBuffer;
  mainCategoryId: number;
  categoryId: number;
  colors: number[];
  sizes: number[];
}

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit {

  //#region Params

  defaultBike: IBikeCreation = {
    bikeId: null,
    brand: null,
    model: null,
    isInStock: true,
    price: null,
    thumbImgContent: null,
    mainCategoryId: null,
    categoryId: null,
    colors: [],
    sizes: []
  }

  @ViewChild('agGrid', { static: false }) agGrid: AgGridAngular;

  bike: IBikeCreation = null;
  private rowData: any = [];
  private gridApi;
  private gridColumnApi;
  private defaultColDef;
  public modules: Module[] = AllCommunityModules;
  private domLayout;
  private rowHeight;
  private frameworkComponents: any;

  constructor(private http: HttpClient, private dialog: MatDialog, private isLoadingService: IsLoadingService) {

    this.defaultColDef = {
      enableRowGroup: true,
      enablePivot: true,
      enableValue: true,
      sortable: true,
      filter: true,
      resizable: true
    };
    this.domLayout = "autoHeight";
    this.rowHeight = 34;
    this.frameworkComponents = {
      buttonRenderer: ButtonRenderComponent,
    }
  }

  //#region Helpers

  async toBase64(file: File): Promise<string | ArrayBuffer> {
    return new Promise<string | ArrayBuffer>((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = error => reject(error);
    });
  }

  async createFormData(bike: IBikeCreation, fileArr: any): Promise<FormData> {
    const formData = new FormData();
    // put all files
    fileArr.map((file, index) => {
      return formData.append('file' + index, file.item, file.item.name);
    });

    // set one img as Thumb
    let file = fileArr.filter(x => x.isThumbImg)[0];
    let base64String = file ? (await this.toBase64(file.item)) : '';
    formData.append('thumbBase64', base64String.toString());

    for (var key in bike) {
      formData.append(key, bike[key]);
    }

    return formData;
  }

  //#endregion


  columnDefs = [
    {
      headerName: 'Name',
      field: 'brand',
      sortable: true,
      filter: true,
      //checkboxSelection: true,
      //suppressSizeToFit: true,
      //minWidth: 200,
      //maxWidth: 350,
      flex: 2
    },
    {
      headerName: 'Model',
      field: 'model',
      sortable: true,
      filter: true,
      flex: 3
    },
    {
      headerName: 'Price',
      field: 'price',
      sortable: true,
      filter: true,
      flex: 1,
      //valueFormatter: params => {
      //  return Math.floor(params.value)
      //    .toString()
      //    .replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
      //}
    },
    {
      headerName: 'Is in stock',
      field: 'isInStock',
      sortable: true,
      filter: true,
      flex: 1,
      cellRenderer: params => {
        return `<input type='checkbox' ${params.value ? 'checked' : ''} />`;
      }
    },
    {
      headerName: '',
      minWidth: 60,
      maxWidth: 60,
      cellRenderer: 'buttonRenderer' ,
      cellRendererParams: {
        onClick: this.editBike.bind(this),
        label: 'Click'
      }
    }
  ];
  //#endregion Params

  //#region CRUD

  // create new bike
  private openAddDialog(bike: IBikeCreation) {
    const dialogRef = this.dialog.open(AdminModalComponent, {
      width: '90%',
      disableClose: true,
      data: {
        data: bike,
        isEdit: false
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const formDataPromise = this.createFormData(result.bike, result.fileArr);
        formDataPromise.then(bike => {
          this.http.post('api/bikes/bike', bike).subscribe(
            data => {
              this.getBikeList();
            },
            error => {
              console.log('oops', error);
              this.openAddDialog(result.bike);
            }
          );

        })
      }
    });
  }

  addBike() {
    const bike = Object.assign({}, this.defaultBike);
    this.openAddDialog(bike);
  }

  private openEditDialog(bike: IBikeCreation) {
    const dialogRef = this.dialog.open(AdminModalComponent, {
      width: '90%',
      disableClose: true,
      data: {
        data: bike,
        isEdit: true
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let formDataPromise = this.createFormData(result.bike, result.fileArr);
        formDataPromise.then(bike => {
          this.http.post('api/bikes/bike', bike).subscribe(
            data => {
              this.getBikeList();
            },
            error => {
              console.log('oops', error);
              this.openEditDialog(result.bike);
            }
          );

        })
      }
    });
  }

  editBike(e) {
    this.openEditDialog(e.rowData);
  }

  //#endregion CRUD

  //#region Init
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.addEventListener('click', this.editBike)
    ;
  }

  onFirstDataRendered(params) {
    params.api.sizeColumnsToFit();
  }

  getBikeList() {
    let promise = this.http.get("api/admin").subscribe(data => {
      this.rowData = data;
    }, error => { console.log(error) }
    )
    this.isLoadingService.add(promise);
  }

  ngOnInit() {
    this.getBikeList();

  }
  //#endregion Init
}
