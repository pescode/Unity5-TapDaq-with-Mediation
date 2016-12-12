//
//  TDMUAPortrait.m
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 03/10/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import "TDMUAPortrait.h"

@implementation TDMUAPortrait

- (TDMUAPortrait *) init{
    TDMUAPortrait *result = nil;
    NSArray* elements = [[NSBundle mainBundle] loadNibNamed: NSStringFromClass([self class]) owner:self options: nil];
    for (id anObject in elements)
    {
        if ([anObject isKindOfClass:[self class]])
        {
            result = anObject;
            break;
        }
    }
    return result;
}

@end
