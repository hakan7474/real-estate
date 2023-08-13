import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EstateService } from '../services/estate.service';
import { TypeService } from '../services/type.service';
import { MESSSAGE, TYPES } from '../_const/const';
import { APIResponse, APIResponseStatus } from '../services/wrappers/api-response';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NotificationService } from '../services/notification.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'estate-add-edit',
  templateUrl: './estate-add-edit.component.html',
  styleUrls: ['./estate-add-edit.component.scss']
})
export class EstateAddEditComponent implements OnInit {

  estateForm: FormGroup
  estateTypes: any[] = [];

  constructor(private _fb: FormBuilder,
    private _estateService: EstateService,
    private _typeService: TypeService,
    private _dialogRef: MatDialogRef<EstateAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _notificationService: NotificationService,
    private datePipe: DatePipe) {

    this.estateForm = this._fb.group({
      EstateId: [null],
      EstateCode: [null, [Validators.required]],
      EstateName: [null, [Validators.required]],
      FloorNumber: [null, [Validators.required]],
      BuildingDate: [null],
      Price: [null],
      RoomCount: [null, [Validators.required]],
      GrossArea: [null],
      NetArea: [null],
      EstateTypeId: [null, [Validators.required]],
      Country: [null],
      City: [null],
      State: [null],
      District: [null],
      Location: [null],
      Address: [null],
    })
  }

  ngOnInit() {
    this.loadData();
    this.estateForm.patchValue(this.data);
  }

  loadData() {
    this.getTypes();
  }

  getTypes() {
    this._typeService.getTypes(TYPES.ESTATE_TYPE).subscribe({
      next: (res: APIResponse) => {
        if (res.Status == APIResponseStatus.Success) {
          this.estateTypes = res.Data;
        } else {
          this._typeService.errorWithDictionary(res.Messages);
        }
      },
      error: (err: any) => {
        console.log(err);
        this._typeService.errorWithDictionary(err.error.Messages);
      }
    })
  }

  onSave() {
    if (this.estateForm.valid) {
      if (this.estateForm.value.BuildingDate) {
        this.estateForm.value.BuildingDate = this.datePipe.transform(this.estateForm.value.BuildingDate, 'yyyy-MM-dd');
      }
      if (this.data) {
        this._estateService.updateEstate(this.estateForm.value).subscribe({
          next: (res: APIResponse) => {
            if (res.Status == APIResponseStatus.Success) {
              this._notificationService.openSnackBar(MESSSAGE.UPDATE_ESTATE_SUCCESS);
              this._dialogRef.close(true);
            } else {
              this._estateService.errorWithDictionary(res.Messages);
            }
          },
          error: (err: any) => {
            console.log(err);
            this._estateService.errorWithDictionary(err.error.Messages);
          }
        })
      } else {
        this._estateService.addEstate(this.estateForm.value).subscribe({
          next: (res: APIResponse) => {
            if (res.Status == APIResponseStatus.Success) {
              this._notificationService.openSnackBar(MESSSAGE.ADD_ESTATE_SUCCESS);
              this._dialogRef.close(true);
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
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.estateForm.controls[controlName].hasError(errorName);
  }

}
