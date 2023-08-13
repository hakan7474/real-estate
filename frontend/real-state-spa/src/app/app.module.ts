import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { EstateAddEditComponent } from './estate-add-edit/estate-add-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EstateService } from './services/estate.service';
import { TypeService } from './services/type.service';
import { BaseService } from './services/base.service';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NotificationService } from './services/notification.service';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatDividerModule } from '@angular/material/divider';
import { DatePipe, registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
@NgModule({
  declarations: [
    AppComponent,
    EstateAddEditComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatDividerModule
  ],
  providers: [
    BaseService,
    NotificationService,
    TypeService,
    EstateService,
    DatePipe,
    { provide: LOCALE_ID, useValue: 'tr' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor() {
    registerLocaleData(localeTr, 'tr');
  }
}
