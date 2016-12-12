//
//  AdNetworkDebugViewController.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 01/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <UIKit/UIKit.h>

#if defined(TAPDAQ_DEV) || defined(TAPDAQ_LOCAL)
#import "Tapdaq.h"
#else
#import <Tapdaq/Tapdaq.h>
#endif

#import "AdNetworkTableViewController.h"

//Debug flags
//#define TDMDEBUG 1

#if defined(TDMDEBUG)
#define TDMDebugLog(fmt, ...) NSLog((@"[%@] " fmt), [self class], ##__VA_ARGS__)
#else
#   define TDMDebugLog(...)
#endif

@interface AdNetworkDebugViewController : UIViewController <TapdaqDelegate>

@property (weak, nonatomic) IBOutlet UIButton *fetchAd;
@property (weak, nonatomic) IBOutlet UIButton *showAd;
@property (weak, nonatomic) IBOutlet UILabel *debug;
@property (weak, nonatomic) IBOutlet UISegmentedControl *adType;

@property (weak, nonatomic) IBOutlet UILabel *credentialsDownloadedLabel;
@property (weak, nonatomic) IBOutlet UIButton *credentialsDownloadedImage;
@property (weak, nonatomic) IBOutlet UIButton *usingAdapterImage;
@property (weak, nonatomic) IBOutlet UILabel *usingAdapterLabel;
@property (weak, nonatomic) IBOutlet UIButton *initialisedImage;
@property (weak, nonatomic) IBOutlet UILabel *initialisedLabel;
@property (weak, nonatomic) IBOutlet UILabel *versionLabel;


@property (weak, nonatomic) IBOutlet UIBarButtonItem *refresh;
@property (weak, nonatomic) IBOutlet UINavigationItem *navigationItem;

@property (assign, atomic) NSString *adNetwork;

@end
