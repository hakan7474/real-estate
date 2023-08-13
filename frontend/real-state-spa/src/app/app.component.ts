import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EstateAddEditComponent } from './estate-add-edit/estate-add-edit.component';
import { EstateService } from './services/estate.service';
import { APIResponse, APIResponseStatus } from './services/wrappers/api-response';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MESSSAGE } from './_const/const';
import { NotificationService } from './services/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  displayedColumns: string[] = [
    'SysCode',
    'EstateCode',
    'EstateName',
    'FloorNumber',
    'BuildingDate',
    'Price',
    'RoomCount',
    'GrossArea',
    'NetArea',
    'EstateTypeName',
    'action'
  ];

  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _dialog: MatDialog,
    private _estateService: EstateService,
    private _notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.getEstates();
  }

  getEstates() {
    this._estateService.getEstates().subscribe({
      next: (res: APIResponse) => {
        if (res.Status == APIResponseStatus.Success) {
          this.dataSource = new MatTableDataSource(res.Data);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        } else {
          this._estateService.errorWithDictionary(res.Messages);
        }
      },
      error: (err: any) => {
        console.log(err);
        this._estateService.errorWithDictionary(err.error.Messages);
      }
    })
  }

  openAddEstateForm() {
    const dialogRef = this._dialog.open(EstateAddEditComponent, {
      width: '50%'
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getEstates();
        }
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }



  deleteEstate(estateId: any) {
    this._notificationService.openDialog().subscribe(confirm => {
      if (confirm) {
        this._estateService.deleteEstate(estateId).subscribe({
          next: (res: APIResponse) => {
            if (res.Status == APIResponseStatus.Success) {
              this._notificationService.openSnackBar(MESSSAGE.DELETE_ESTATE_SUCCESS, 'done');
              this.getEstates();
            } else {
              this._estateService.errorWithDictionary(res.Messages);
            }
          },
          error: (err: any) => {
            console.log(err);

            this._estateService.errorWithDictionary(err.error.Messages);
          }
        })
      }
    })



  }

  openEditForm(data: any) {
    const dialogRef = this._dialog.open(EstateAddEditComponent, {
      data: data,
      width: '50%'
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getEstates();
        }
      },
    });
  }

}
