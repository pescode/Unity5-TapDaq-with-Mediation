//
//  TDMUAPortrait.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 03/10/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface TDMUAPortrait : UIView

@property (weak, nonatomic) IBOutlet UIImageView *mainAd;
@property (weak, nonatomic) IBOutlet UILabel *appName;
@property (weak, nonatomic) IBOutlet UILabel *ratingCount;
@property (weak, nonatomic) IBOutlet UIButton *infoButton;
@property (weak, nonatomic) IBOutlet UIButton *closeButton;
@property (weak, nonatomic) IBOutlet UIImageView *appIcon;
@property (weak, nonatomic) IBOutlet UIButton *adClickButton;
@property (weak, nonatomic) IBOutlet UIImageView *star1;
@property (weak, nonatomic) IBOutlet UIImageView *star2;
@property (weak, nonatomic) IBOutlet UIImageView *star3;
@property (weak, nonatomic) IBOutlet UIImageView *star4;
@property (weak, nonatomic) IBOutlet UIImageView *star5;

@end
