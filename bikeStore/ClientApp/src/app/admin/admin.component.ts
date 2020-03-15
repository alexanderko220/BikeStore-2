import { Component, OnInit , ViewChild} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AgGridAngular } from 'ag-grid-angular';
import { AllCommunityModules, Module } from "@ag-grid-community/all-modules";
import { MatDialog } from '@angular/material/dialog';
import { AdminModalComponent } from './admin-modal/admin-modal.component';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit {

  //#region Params

  @ViewChild('agGrid', { static: false }) agGrid: AgGridAngular;
  private rowData: any = [];
  private gridApi;
  private gridColumnApi;
  private defaultColDef;
  public modules: Module[] = AllCommunityModules;
  private domLayout;
  private rowHeight;

  constructor(private http: HttpClient,
              private dialog: MatDialog,
   
  ) {
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
    
  }

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
      flex: 1
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
      cellRenderer: function () {
        var eGui = document.createElement("div");
        eGui.innerHTML = `<button type="button" class="btn btn-outline-warning btn-sm"><i class="far fa-edit"></i></button>`;
        return eGui;
      }
    }
  ];
  //#endregion Params

  //#region CRUD

  openAddDialog() {
    const dialogRef = this.dialog.open(AdminModalComponent, {
      width: '90%',
      disableClose: true
       });
    
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  //#endregion CRUD

  //#region Init
  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }
  
  onFirstDataRendered(params) {
    params.api.sizeColumnsToFit();
  }

  ngOnInit() {
    this.http.get("api/admin").subscribe(response => {
      
      this.rowData = response;
      
    })
  }
  //#endregion Init
}
