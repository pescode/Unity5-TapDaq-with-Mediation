//
//  AdNetworkTableViewController.m
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 01/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import "AdNetworkDebugViewController.h"

@interface AdNetworkTableViewController ()

@property (retain) NSArray *adNetworkArray;
@property NSString *adNetworkSelected;

@end

@implementation AdNetworkTableViewController

@synthesize adNetworkArray;


- (void)viewDidLoad {
    [super viewDidLoad];
    
    // Uncomment the following line to preserve selection between presentations.
    // self.clearsSelectionOnViewWillAppear = NO;
    
    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
    
    adNetworkArray = @[@"Mediated", @"AdColony", @"AdMob", @"AppLovin", @"Chartboost", @"Facebook", @"InMobi", @"UnityAds", @"Vungle"];
    
    UITableView *adNetworkTableView = [[UITableView alloc] initWithFrame:self.view.bounds style:UITableViewStyleGrouped];
    adNetworkTableView.dataSource = self;
    adNetworkTableView.delegate = self;
    
    [adNetworkTableView reloadData];
    
    //Removed: Was displaying table twice
    //[self.view addSubview:adNetworkTableView];
    
    TDMDebugLog(@"table view loaded");
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark - Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return [self.adNetworkArray count];
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    static NSString *cellIdentifier = @"AdNetworkCell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    
    if (nil == cell)
    {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:cellIdentifier];
    }
    
    cell.textLabel.text = [self.adNetworkArray objectAtIndex:indexPath.row];
    
    TDMNetwork networkToCheck;
    
    if ([cell.textLabel.text isEqualToString:@"AdColony"]) {
        networkToCheck = TDMAdColony;
    }else if ([cell.textLabel.text isEqualToString:@"AdMob"]) {
        networkToCheck = TDMAdMob;
    }else if ([cell.textLabel.text isEqualToString:@"AppLovin"]) {
        networkToCheck = TDMApplovin;
    }else if ([cell.textLabel.text isEqualToString:@"Chartboost"]) {
        networkToCheck = TDMChartboost;
    }else if ([cell.textLabel.text isEqualToString:@"Facebook"]) {
        networkToCheck = TDMFacebookAudienceNetwork;
    }else if ([cell.textLabel.text isEqualToString:@"InMobi"]) {
        networkToCheck = TDMInMobi;
    }else if ([cell.textLabel.text isEqualToString:@"UnityAds"]) {
        networkToCheck = TDMUnityAds;
    }else if ([cell.textLabel.text isEqualToString:@"Vungle"]) {
        networkToCheck = TDMVungle;
    }else if ([cell.textLabel.text isEqualToString:@"Mediated"]) {
        networkToCheck = TDMMediatedNetwork;
    }
    
    //If the FW is not present then the cell should not be selectable
    //if ([[Tapdaq sharedSession] isAdNetworkSetup:networkToCheck]) {
    cell.accessoryType = UITableViewCellAccessoryDisclosureIndicator;
    //}
    
    return cell;
}

- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section
{
    return @"Select an ad network:";
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    
    self.adNetworkSelected = [adNetworkArray objectAtIndex:indexPath.row];
    
    [self performSegueWithIdentifier:@"AdNetworkDebugSegue" sender:self];
    
}

#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
    
    TDMDebugLog(@"TDMTable: prepareForSegue");
    
    if ([[segue identifier] isEqualToString:@"AdNetworkDebugSegue"]) {
        
        AdNetworkDebugViewController *vc = [segue destinationViewController];
        
        vc.adNetwork = self.adNetworkSelected;
    }
}

-(void) viewWillDisappear:(BOOL)animated {
    
    TDMDebugLog(@"TDMTable: viewWillDisappear");
    
    if ([self.navigationController.viewControllers indexOfObject:self]==NSNotFound) {
        // Navigation button was pressed. Do some stuff
        [self.navigationController popViewControllerAnimated:YES];
    }
    
    //Note: Doesn't leave the developer app cleanly - sort this out later
    
    [super viewWillDisappear:animated];
    
    //[self dismissViewControllerAnimated:YES completion:nil];
}

/*-(BOOL)prefersStatusBarHidden{
 return YES;
 }*/

@end
