import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {DashboardComponent} from './dashboard/dashboard.component';
import {ConfigComponent} from './config/config.component';
import {ClientTreeComponent} from './client-tree/client-tree.component';

export const routes: Routes = [
    {path: 'dashboard', component: DashboardComponent},
    {path: 'config', component: ConfigComponent},
    {path: 'tree', component: ClientTreeComponent},
    {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
    {path: '**', component: DashboardComponent}
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
