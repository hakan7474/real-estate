import { MatSnackBar } from '@angular/material/snack-bar';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private _dialog: MatDialog,
    private _snackBar: MatSnackBar) { }

  openSnackBar(message: string, action: string = 'OK') {
    this._snackBar.open(message, action, {
      duration: 5000,
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }


  openDialog(message?: string) {
    const dialogRef = this._dialog.open(ConfirmDialogComponent, {
      data: {
        message: message ?? 'Are you sure want to delete?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No'
        }
      }
    });

    return dialogRef.afterClosed();
  }

}
