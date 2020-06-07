import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AgGridAngular } from 'ag-grid-angular';
import { AllCommunityModules, Module } from "@ag-grid-community/all-modules";
import { MatDialog } from '@angular/material/dialog';
import { AdminModalComponent } from './admin-modal/admin-modal.component';
import { IsLoadingService } from '@service-work/is-loading';
import { ButtonRenderComponent } from '../ag-render/button-render/button-render.component';
import { FileHelperService } from "../services/fileHelper.service";
import { IBikeCreation, IBikeDto } from "../interfaces/interfaces";
import { DOCUMENT, LocationStrategy } from '@angular/common';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/operators';

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
    thumbBase64: null,
    mainCategoryId: null,
    categoryId: null,
    colors: [],
    sizes: [],
    thumbFileName: null,
    junkColors: [],
    junkSizes: []
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

  constructor(private http: HttpClient, private dialog: MatDialog, private isLoadingService: IsLoadingService,
    private fileService: FileHelperService) {

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
      buttonRenderer: ButtonRenderComponent
    }
   
  }

  //#region Helpers

  async createFormData(bike: IBikeCreation, fileArr: any): Promise<FormData> {
    const formData = new FormData();
    // put all files
    fileArr.map((file) => {
      return formData.append('files[]', file.item, file.item.name);
    });

    // set one img as Thumb
    const file = fileArr.filter(x => x.isThumbImg)[0];
    bike.thumbBase64 = file ? (await this.fileService.toBase64(file.item)) : null;
    bike.thumbFileName = file ? file.item.name : null;
   // if (base64String) bike.thumbBase64 = base64String.toString();

    //bike.junkColors.forEach(jC => {
    //  jC.bike = null;
    //  jC.color = null;
    //});

    //bike.junkSizes.forEach(jS => {
    //  jS.bike = null;
    //  jS.size = null;
    //});


    formData.append('bike', JSON.stringify(bike));

    return formData;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };

  //#endregion
  columnDefs = [
    {
      headerName: 'Category',
      field: 'categoryName',
      sortable: true,
      filter: true,
      flex: 2
    },
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
      cellRenderer: 'buttonRenderer',
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
      if (result && result !== 'false') {
        const formDataPromise = this.createFormData(result.bike, result.fileArr);
        formDataPromise.then(bike => {
          this.http.post('api/bikes/bike', bike)
            .pipe(
              catchError(this.handleError)
            )
            .subscribe(
            data => {
              this.getBikeList();
            },
            error => {
              console.log('oops', error);
              this.openAddDialog(result.bike);
            }
          );

        });
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
      if (result && result !== 'false') {
        const formDataPromise = this.createFormData(result.bike, result.fileArr);
        formDataPromise.then(bike => {
          this.http.put(`api/bikes/bike`, bike)
            .pipe(
              catchError(this.handleError)
            )
            .subscribe(
              data => {
                this.getBikeList();
              },
              error => {
                console.log('oops', error);
                this.openEditDialog(result.bike);
              }
            );
        });
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
    this.gridApi.addEventListener('click', this.editBike);
  }

  onFirstDataRendered(params) {
    params.api.sizeColumnsToFit();
  }

  getBikeList() {
    const promise = this.http.get<IBikeDto>("api/admin").subscribe(data => {
        this.rowData = data;
      },
      error => {
        console.log(error);
      });
    this.isLoadingService.add(promise);
  }

  ngOnInit() {
    this.getBikeList();

  }
  //#endregion Init
}
