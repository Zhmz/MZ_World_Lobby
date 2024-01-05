//
//  QuickpassModel.m
//  UnityFramework
//
//  Created by 罗礼豪 on 2021/9/10.
//

#import "QuickpassModel.h"

@interface QuickpassModel()

@property (nonatomic , copy) NSDictionary *option;
@property (nonatomic , strong) NSMutableDictionary *callBackDict;

@end

@implementation QuickpassModel

- (NTESQuickLoginModel *)setupModel:(NSDictionary *)dict config:(ConfigHandler)config{
    self.option = dict;
    
    configHandler = config;
    self.callBackDict = [NSMutableDictionary dictionary];
    NTESQuickLoginModel *customModel = [[NTESQuickLoginModel alloc] init];
    customModel.customViewBlock = ^(UIView * _Nullable customView) {
        [customView setNeedsLayout];
        NSArray *widgets = [dict objectForKey:@"widgets"];
        for (NSInteger i = 0; i < widgets.count; i++) {
            NSDictionary *widgetsDict = widgets[i];
            NSString *type = [widgetsDict objectForKey:@"type"];
            if ([type isEqualToString:@"UIButton"]) {
//                int buttonType = [[widgetsDict objectForKey:@"UIButtonType"] intValue];
                NSString *title = [widgetsDict objectForKey:@"title"];
                NSString *titleColor = [widgetsDict objectForKey:@"titleColor"];
                int titleFont = [[widgetsDict objectForKey:@"titleFont"] intValue];
                int cornerRadius = [[widgetsDict objectForKey:@"cornerRadius"] intValue];
                NSDictionary *frame = [widgetsDict objectForKey:@"frame"];
                NSString *image = [widgetsDict objectForKey:@"image"];
                NSString *backgroundColor = [widgetsDict objectForKey:@"backgroundColor"];
                NSString *backgroundImage = [widgetsDict objectForKey:@"backgroundImage"];
                
                int mainScreenLeftDistance = [[frame objectForKey:@"mainScreenLeftDistance"] intValue];
                int mainScreenRightDistance = [[frame objectForKey:@"mainScreenRightDistance"] intValue];
                int mainScreenCenterXWithLeftDistance = [[frame objectForKey:@"mainScreenCenterXWithLeftDistance"] intValue];
                int mainScreenBottomDistance = [[frame objectForKey:@"mainScreenBottomDistance"] intValue];
                int mainScreenTopDistance = [[frame objectForKey:@"mainScreenTopDistance"] intValue];
                int width = [[frame objectForKey:@"width"] intValue];
                int height = [[frame objectForKey:@"height"] intValue];

                UIButton *button = [UIButton buttonWithType:UIButtonTypeCustom];
                [button setImage:[self loadImageWithName:image] forState:UIControlStateNormal];
                [button addTarget:self action:@selector(buttonDidTipped:) forControlEvents:UIControlEventTouchUpInside];
                button.tag = i;
                [button setTitle:title forState:UIControlStateNormal];
                [button setTitleColor:[self ntes_colorWithHexString:titleColor] forState:UIControlStateNormal];
                button.titleLabel.font = [UIFont systemFontOfSize:titleFont];
                button.layer.cornerRadius = cornerRadius;
                button.layer.masksToBounds = YES;
                button.backgroundColor = [self ntes_colorWithHexString:backgroundColor];
                [button setBackgroundImage:[self loadImageWithName:backgroundImage] forState:UIControlStateNormal];
                [customView addSubview:button];
                if (mainScreenLeftDistance > 0 && mainScreenRightDistance > 0) {
                    if (mainScreenTopDistance > 0) {
                        button.frame = CGRectMake(mainScreenLeftDistance, mainScreenTopDistance, [UIScreen mainScreen].bounds.size.width - mainScreenLeftDistance - mainScreenRightDistance, height);
                    } else {
                        button.frame = CGRectMake(mainScreenLeftDistance, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, [UIScreen mainScreen].bounds.size.width - mainScreenLeftDistance - mainScreenLeftDistance, height);
                    }
                } else if (mainScreenLeftDistance == 0 && mainScreenRightDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        button.frame = CGRectMake(([UIScreen mainScreen].bounds.size.width - width)/2 - mainScreenCenterXWithLeftDistance, mainScreenTopDistance,width, height);
                    } else {
                        button.frame = CGRectMake(([UIScreen mainScreen].bounds.size.width - width)/2, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                } else if (mainScreenLeftDistance > 0 && mainScreenRightDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        button.frame = CGRectMake(mainScreenLeftDistance, mainScreenTopDistance, width, height);
                    } else {
                        button.frame = CGRectMake(mainScreenLeftDistance, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                } else if (mainScreenRightDistance > 0 && mainScreenLeftDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        button.frame = CGRectMake([UIScreen mainScreen].bounds.size.width - mainScreenRightDistance - width, mainScreenTopDistance, width, height);
                    } else {
                        button.frame = CGRectMake([UIScreen mainScreen].bounds.size.width - mainScreenRightDistance - width, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                }
               
            } else if ([type isEqualToString:@"UILabel"]) {
                NSDictionary *widgetsDict = widgets[i];
                NSString *type = [widgetsDict objectForKey:@"type"];
                NSString *text = [widgetsDict objectForKey:@"text"];
                NSString *textColor = [widgetsDict objectForKey:@"textColor"];
                NSString *backgroundColor = [widgetsDict objectForKey:@"backgroundColor"];
                int font = [[widgetsDict objectForKey:@"font"] intValue];
                int textAlignment = [[widgetsDict objectForKey:@"textAlignment"] intValue];
                int cornerRadius = [[widgetsDict objectForKey:@"cornerRadius"] intValue];
                NSDictionary *frame = [widgetsDict objectForKey:@"frame"];

                int mainScreenLeftDistance = [[frame objectForKey:@"mainScreenLeftDistance"] intValue];
                int mainScreenRightDistance = [[frame objectForKey:@"mainScreenRightDistance"] intValue];
                int mainScreenCenterXWithLeftDistance = [[frame objectForKey:@"mainScreenCenterXWithLeftDistance"] intValue];
                int mainScreenBottomDistance = [[frame objectForKey:@"mainScreenBottomDistance"] intValue];
                int mainScreenTopDistance = [[frame objectForKey:@"mainScreenTopDistance"] intValue];
                int width = [[frame objectForKey:@"width"] intValue];
                int height = [[frame objectForKey:@"height"] intValue];

                Class class = NSClassFromString(type);
                UILabel *label = (UILabel *)[[class alloc] init];
                label.tag = i;
                UITapGestureRecognizer *tap = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(labelDidTipped:)];
                [label addGestureRecognizer:tap];
                label.userInteractionEnabled = YES;
                label.text = text;
                label.layer.cornerRadius = cornerRadius;
                label.layer.masksToBounds = YES;
                label.textColor = [self ntes_colorWithHexString:textColor];
                label.font = [UIFont systemFontOfSize:font];
                label.textAlignment = textAlignment;
                label.backgroundColor = [self ntes_colorWithHexString:backgroundColor];
                [customView addSubview:label];
                CGFloat screenHeight = [UIScreen mainScreen].bounds.size.height;
                if (mainScreenLeftDistance > 0 && mainScreenRightDistance > 0) {
                    if (mainScreenTopDistance > 0) {
                        label.frame = CGRectMake(mainScreenLeftDistance, mainScreenTopDistance, [UIScreen mainScreen].bounds.size.width - mainScreenLeftDistance - mainScreenRightDistance, height);
                    } else {
                        label.frame = CGRectMake(mainScreenLeftDistance, screenHeight - mainScreenBottomDistance -height, [UIScreen mainScreen].bounds.size.width - mainScreenLeftDistance - mainScreenLeftDistance, height);
                    }
                } else if (mainScreenLeftDistance == 0 && mainScreenRightDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        label.frame = CGRectMake(([UIScreen mainScreen].bounds.size.width - width)/2 - mainScreenCenterXWithLeftDistance, mainScreenTopDistance,width, height);
                    } else {
                        label.frame = CGRectMake(([UIScreen mainScreen].bounds.size.width - width)/2, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                } else if (mainScreenLeftDistance > 0 && mainScreenRightDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        label.frame = CGRectMake(mainScreenLeftDistance, mainScreenTopDistance, width, height);
                    } else {
                        label.frame = CGRectMake(mainScreenLeftDistance, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                } else if (mainScreenRightDistance > 0 && mainScreenLeftDistance == 0) {
                    if (mainScreenTopDistance > 0) {
                        label.frame = CGRectMake([UIScreen mainScreen].bounds.size.width - mainScreenRightDistance - width, mainScreenTopDistance, width, height);
                    } else {
                        label.frame = CGRectMake([UIScreen mainScreen].bounds.size.width - mainScreenRightDistance - width, [UIScreen mainScreen].bounds.size.height - mainScreenBottomDistance -height, width, height);
                    }
                }
            }
        }
    };
    
    customModel.backgroundColor = [self ntes_colorWithHexString:[dict objectForKey:@"backgroundColor"]];
    customModel.bgImage = [self loadImageWithName:[dict objectForKey:@"bgImage"]];
    customModel.navTextFont = [UIFont systemFontOfSize:[[dict objectForKey:@"navTextFont"] intValue]];
    customModel.navText = [dict objectForKey:@"navText"];
    customModel.navTextColor = [self ntes_colorWithHexString:[dict objectForKey:@"navTextColor"]];
    customModel.navBgColor = [self ntes_colorWithHexString:[dict objectForKey:@"navBgColor"]];
    customModel.navTextHidden = [[dict objectForKey:@"navTextHidden"] boolValue];
    customModel.logoImg = [self loadImageWithName:[dict objectForKey:@"logoIconName"]];
    customModel.numberColor = [self ntes_colorWithHexString:[dict objectForKey:@"numberColor"]];
    customModel.numberFont = [UIFont systemFontOfSize:[[dict objectForKey:@"numberFont"] intValue]];
    customModel.brandColor = [self ntes_colorWithHexString:[dict objectForKey:@"brandColor"]];
    customModel.brandFont = [UIFont systemFontOfSize:[[dict objectForKey:@"brandFont"] intValue]];
    customModel.brandHidden = [[dict objectForKey:@"brandHidden"] boolValue];
    customModel.brandHeight = [[dict objectForKey:@"brandHeight"] intValue];
    customModel.logBtnTextFont = [UIFont systemFontOfSize:[[dict objectForKey:@"loginBtnTextSize"] intValue]];
    customModel.logBtnText = [dict objectForKey:@"logBtnText"];
    customModel.logBtnTextColor = [self ntes_colorWithHexString:[dict objectForKey:@"logBtnTextColor"]];
    customModel.logBtnUsableBGColor = [self ntes_colorWithHexString:[dict objectForKey:@"logBtnUsableBGColor"]];
    customModel.closePopImg = [self loadImageWithName:[dict objectForKey:@"closePopImg"]];
    customModel.numberBackgroundColor = [self ntes_colorWithHexString:[dict objectForKey:@"numberBackgroundColor"]];
    customModel.numberHeight = [[dict objectForKey:@"numberHeight"] intValue];
    customModel.numberCornerRadius = [[dict objectForKey:@"numberCornerRadius"] intValue];
    customModel.numberLeftContent = [dict objectForKey:@"numberLeftContent"];
    customModel.numberRightContent = [dict objectForKey:@"numberRightContent"];
    customModel.faceOrientation = [[dict objectForKey:@"faceOrientation"] intValue];
//    customModel.loginDidDisapperfaceOrientation = [[dict objectForKey:@"loginDidDisapperfaceOrientation"] intValue];
    customModel.logoHeight = [[dict objectForKey:@"logoHeight"] intValue];
    customModel.logoHidden = [[dict objectForKey:@"logoHidden"] boolValue];
    customModel.modalTransitionStyle = [[dict objectForKey:@"modalTransitionStyle"] intValue];
    customModel.privacyFont = [UIFont systemFontOfSize:[[dict objectForKey:@"privacyFont"] intValue]];
    int prograssHUDBlock = [[dict objectForKey:@"prograssHUDBlock"] intValue];
    if (prograssHUDBlock) {
        customModel.prograssHUDBlock = ^(UIView * _Nullable prograssHUDBlock) {
        };
    }

    int loadingViewBlock = [[dict objectForKey:@"loadingViewBlock"] intValue];
    if (loadingViewBlock) {
        customModel.loadingViewBlock = ^(UIView * _Nullable customLoadingView) {

        };
    }

    customModel.appPrivacyText = [dict objectForKey:@"appPrivacyText"];
    customModel.appFPrivacyText = [dict objectForKey:@"appFPrivacyText"];
    customModel.appFPrivacyURL = [dict objectForKey:@"appFPrivacyURL"];
    customModel.appSPrivacyText = [dict objectForKey:@"appSPrivacyText"];
    customModel.appSPrivacyURL = [dict objectForKey:@"appSPrivacyURL"];
    customModel.appTPrivacyText = [dict objectForKey:@"appTPrivacyText"];
    customModel.appTPrivacyURL = [dict objectForKey:@"appTPrivacyURL"];

    customModel.appFourPrivacyText = [dict objectForKey:@"appFourPrivacyText"];
    customModel.appFourPrivacyURL = [dict objectForKey:@"appFourPrivacyURL"];
    
    
      customModel.appPrivacyOriginLeftMargin = [[dict objectForKey:@"appPrivacyOriginLeftMargin"] doubleValue];
      customModel.appPrivacyOriginRightMargin = [[dict objectForKey:@"appPrivacyOriginRightMargin"] doubleValue];
        customModel.appPrivacyOriginBottomMargin = [[dict objectForKey:@"appPrivacyOriginBottomMargin"] doubleValue];
    
    customModel.appPrivacyOriginTopMargin = [[dict objectForKey:@"appPrivacyOriginTopMargin"] doubleValue];

      customModel.appFPrivacyTitleText = [dict objectForKey:@"appFPrivacyTitleText"];
      customModel.appPrivacyTitleText = [dict objectForKey:@"appPrivacyTitleText"];
      customModel.appSPrivacyTitleText = [dict objectForKey:@"appSPrivacyTitleText"];
      customModel.appPrivacyAlignment = [[dict objectForKey:@"appPrivacyAlignment"] intValue];
      customModel.isOpenSwipeGesture = [[dict objectForKey:@"isOpenSwipeGesture"] boolValue];
      customModel.logBtnOffsetTopY = [[dict objectForKey:@"logBtnOffsetTopY"] doubleValue];
      customModel.logBtnHeight = [[dict objectForKey:@"logBtnHeight"] doubleValue];
      customModel.brandOffsetTopY = [[dict objectForKey:@"brandOffsetTopY"] doubleValue];
      customModel.brandOffsetX = [[dict objectForKey:@"brandOffsetX"] doubleValue];
      customModel.numberOffsetTopY = [[dict objectForKey:@"numberOffsetTopY"] doubleValue];
      customModel.numberOffsetX = [[dict objectForKey:@"numberOffsetX"] doubleValue];
      customModel.checkBoxAlignment = [[dict objectForKey:@"checkBoxAlignment"] intValue];
      customModel.checkBoxMargin = [[dict objectForKey:@"checkBoxMargin"] intValue];
      customModel.checkboxWH = [[dict objectForKey:@"checkboxWH"] intValue];
      customModel.checkedHidden = [[dict objectForKey:@"checkedHidden"] boolValue];
      customModel.checkedImg = [self loadImageWithName:[dict objectForKey:@"checkedImageName"]];
//      customModel.checkedImg  = [UIImage imageWithName:@"checkedImageName" class:[self class]];
      customModel.uncheckedImg = [self loadImageWithName:[dict objectForKey:@"unCheckedImageName"]];
//      customModel.uncheckedImg =  [UIImage imageWithName:@"unCheckedImageName" class:[self class]];
      customModel.logBtnOriginRight = [[dict objectForKey:@"logBtnOriginRight"] intValue];
      customModel.logBtnOriginLeft = [[dict objectForKey:@"logBtnOriginLeft"] intValue];


      customModel.logoOffsetTopY = [[dict objectForKey:@"logoOffsetTopY"] doubleValue];
      customModel.logoOffsetX = [[dict objectForKey:@"logoOffsetX"] doubleValue];
      customModel.logBtnRadius = [[dict objectForKey:@"logBtnRadius"] intValue];
      customModel.logoWidth = [[dict objectForKey:@"logoWidth"] intValue];

      customModel.logoOffsetTopY = [[dict objectForKey:@"logoOffsetTopY"] doubleValue];
      customModel.logoOffsetX = [[dict objectForKey:@"logoOffsetX"] doubleValue];
      customModel.brandBackgroundColor = [self ntes_colorWithHexString:[dict objectForKey:@"brandBackgroundColor"]];
      customModel.privacyColor = [self ntes_colorWithHexString:[dict objectForKey:@"privacyColor"]];
      customModel.protocolColor = [self ntes_colorWithHexString:[dict objectForKey:@"protocolColor"]];

      customModel.privacyNavReturnImg = [self loadImageWithName:[dict objectForKey:@"privacyNavReturnImg"]];
      customModel.navReturnImgHeight = [[dict objectForKey:@"navReturnImgHeight"] intValue];
      customModel.navReturnImgLeftMargin = [[dict objectForKey:@"navReturnImgLeftMargin"] intValue];
      customModel.navReturnImgWidth = [[dict objectForKey:@"navReturnImgWidth"] intValue];
      customModel.videoURL = [dict objectForKey:@"videoURL"];
      customModel.navReturnImgBottomMargin = [[dict objectForKey:@"navReturnImgBottomMargin"] intValue];
      customModel.modalTransitionStyle = [[dict objectForKey:@"modalTransitionStyle"] intValue];
      customModel.navReturnImg = [self loadImageWithName:[dict objectForKey:@"navReturnImg"]];
      customModel.logBtnHighlightedImg = [self loadImageWithName:[dict objectForKey:@"logBtnHighlightedImg"]];
      customModel.navBarHidden = [[dict objectForKey:@"navBarHidden"] boolValue];
      customModel.logBtnEnableImg = [self loadImageWithName:[dict objectForKey:@"logBtnEnableImg"]];
      int shouldHiddenPrivacyMarks = [[dict objectForKey:@"shouldHiddenPrivacyMarks"] intValue];
      if (shouldHiddenPrivacyMarks) {
          customModel.shouldHiddenPrivacyMarks = YES;
      } else {
          customModel.shouldHiddenPrivacyMarks = NO;
      }

      int navControl = [[dict objectForKey:@"navControl"] intValue];
      int navControlRightMargin = [[dict objectForKey:@"navControlRightMargin"] intValue];
      int navControlBottomMargin = [[dict objectForKey:@"navControlBottomMargin"] intValue];
      int navControlWidth = [[dict objectForKey:@"navControlWidth"] intValue];
      int navControlHeight = [[dict objectForKey:@"navControlHeight"] intValue];

      if (navControl) {
          UIView *view = [[UIView alloc] initWithFrame:CGRectMake(0, 0, 40, 40)];
          customModel.navControlRightMargin = navControlRightMargin;
          customModel.navControlBottomMargin = navControlBottomMargin;
          customModel.navControlWidth = navControlWidth;
          customModel.navControlHeight = navControlHeight;
          view.backgroundColor = [UIColor redColor];
          customModel.navControl = view;
      }

      int statusBarStyle = [[dict objectForKey:@"statusBarStyle"] intValue];
      if (statusBarStyle) {
          customModel.statusBarStyle = statusBarStyle;
      }

    customModel.isRepeatPlay = [[dict objectForKey:@"isRepeatPlay"] boolValue];

    //           customModel.faceOrientation = UIInterfaceOrientationLandscapeLeft;
    customModel.animationRepeatCount = [[dict objectForKey:@"animationRepeatCount"] integerValue];
    NSArray *animationImages = [dict objectForKey:@"animationImages"];
    NSMutableArray *array = [NSMutableArray array];
    for (NSDictionary *image in animationImages) {
        [array addObject:[self loadImageWithName:[image objectForKey:@"imageName"]]];
    }
   customModel.animationImages = array;
   customModel.animationDuration = [[dict objectForKey:@"animationDuration"] integerValue];
    customModel.privacyState = [[dict objectForKey:@"privacyState"] boolValue];

    int authWindowPop = [[dict objectForKey:@"authWindowPop"] intValue];
    if (authWindowPop == 0) {
        customModel.authWindowPop = NTESAuthWindowPopFullScreen;
    } else if (authWindowPop == 1) {
        customModel.authWindowPop = NTESAuthWindowPopCenter;
    } else {
        customModel.authWindowPop = NTESAuthWindowPopBottom;
    }

    int closePopImgHeight = [[dict objectForKey:@"closePopImgHeight"] intValue];
    int closePopImgWidth = [[dict objectForKey:@"closePopImgWidth"] intValue];
    customModel.closePopImgWidth = closePopImgWidth;
    customModel.closePopImgHeight = closePopImgHeight;

    int closePopImgOriginY = [[dict objectForKey:@"closePopImgOriginY"] intValue];
    int closePopImgOriginX = [[dict objectForKey:@"closePopImgOriginX"] intValue];
    customModel.closePopImgOriginX = closePopImgOriginX;
    customModel.closePopImgOriginY = closePopImgOriginY;

    int authWindowCenterOriginX = [[dict objectForKey:@"authWindowCenterOriginX"] intValue];
    int authWindowCenterOriginY = [[dict objectForKey:@"authWindowCenterOriginY"] intValue];
    customModel.authWindowCenterOriginY = authWindowCenterOriginY;
    customModel.authWindowCenterOriginX = authWindowCenterOriginX;

    int popCenterCornerRadius = [[dict objectForKey:@"popCenterCornerRadius"] intValue];
    int popBottomCornerRadius = [[dict objectForKey:@"popBottomCornerRadius"] intValue];
    customModel.popBottomCornerRadius = popBottomCornerRadius;
    customModel.popCenterCornerRadius = popCenterCornerRadius;
    customModel.presentDirectionType = NTESPresentDirectionPresent;

    customModel.popBackgroundColor = [[self ntes_colorWithHexString:[dict objectForKey:@"popBackgroundColor"]] colorWithAlphaComponent:[[dict objectForKey:@"alpha"] doubleValue]];
    
    
    UIViewController *rootController = [UIApplication sharedApplication].delegate.window.rootViewController;
    customModel.currentVC = rootController;
    customModel.rootViewController = rootController;
 
   
    customModel.backActionBlock = ^(int backType) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        [dict setValue:@"backAction" forKey:@"action"];
        [dict setValue:@(backType) forKey:@"backType"];
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        configHandler([jsonStirng UTF8String]);
    };

    customModel.closeActionBlock = ^{
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        [dict setValue:@"closeAction" forKey:@"action"];
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        configHandler([jsonStirng UTF8String]);
         
    };
    customModel.loginActionBlock = ^(BOOL isChecked) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        [dict setValue:@"loginAction" forKey:@"action"];
        [dict setValue:@(isChecked) forKey:@"checked"];
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        configHandler([jsonStirng UTF8String]);
        
    };
    customModel.checkActionBlock = ^(BOOL isChecked) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        [dict setValue:@"checkedAction" forKey:@"action"];
        [dict setValue:@(isChecked) forKey:@"checked"];
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        configHandler([jsonStirng UTF8String]);
        

    };
    customModel.privacyActionBlock = ^(int privacyType) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        NSString *privacy;
        if (privacyType == 0) {
            privacy = @"appDPrivacy";
        } else if (privacyType == 1) {
            privacy = @"appFPrivacy";
        } else if (privacyType == 2) {
            privacy = @"appSPrivacy";
        }
        [dict setValue:privacy forKey:@"action"];
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        configHandler([jsonStirng UTF8String]);
    };
    
    CGFloat authWindowHeight = [[dict objectForKey:@"authWindowHeight"] floatValue];
    customModel.authWindowHeight = authWindowHeight;
    CGFloat authWindowWidth = [[dict objectForKey:@"authWindowWidth"] floatValue];
    customModel.authWindowWidth = authWindowWidth;
    customModel.contentMode = [[dict objectForKey:@"contentMode"] intValue];
    customModel.appPrivacyLineSpacing =  [[dict objectForKey:@"appPrivacyLineSpacing"] intValue];
    customModel.appPrivacyWordSpacing =  [[dict objectForKey:@"appPrivacyWordSpacing"] intValue];
    return customModel;
}

- (void)buttonDidTipped:(UIButton *)sender {
    NSArray *option = [self.option objectForKey:@"widgets"];
    NSDictionary *action = option[sender.tag];
    NSString *actions = [action objectForKey:@"action"];
    [self.callBackDict setValue:actions forKey:@"action"];
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self.callBackDict options:NSJSONWritingPrettyPrinted error:nil];
    NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    configHandler([jsonStirng UTF8String]);
}

- (void)labelDidTipped:(UITapGestureRecognizer *)tap {
    UILabel *label = (UILabel *)tap.view;
    NSArray *option = [self.option objectForKey:@"widgets"];
    NSDictionary *action = option[label.tag];
    NSString *actions = [action objectForKey:@"action"];
    [self.callBackDict setValue:actions forKey:@"action"];
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self.callBackDict options:NSJSONWritingPrettyPrinted error:nil];
    NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    configHandler([jsonStirng UTF8String]);
}

- (nullable UIColor *)ntes_colorWithHexString:(NSString *)string {
    return [self ntes_colorWithHexString:string alpha:1.0f];
}

- (nullable UIColor *)ntes_colorWithHexString:(NSString *)string alpha:(CGFloat)alpha {
    NSString *pureHexString = [[string stringByTrimmingCharactersInSet:[NSCharacterSet whitespaceAndNewlineCharacterSet]] uppercaseString];
    if ([pureHexString hasPrefix:[@"#" uppercaseString]] || [pureHexString hasPrefix:[@"#" lowercaseString]]) {
        pureHexString = [pureHexString substringFromIndex:1];
    }

    CGFloat r, g, b, a;
    if (ntes_hexStrToRGBA(string, &r, &g, &b, &a)) {
        return [UIColor colorWithRed:r green:g blue:b alpha:a];
    }
    return nil;
}

static BOOL ntes_hexStrToRGBA(NSString *str, CGFloat *r, CGFloat *g, CGFloat *b, CGFloat *a) {
    NSCharacterSet *set = [NSCharacterSet whitespaceAndNewlineCharacterSet];
    str = [[str stringByTrimmingCharactersInSet:set] uppercaseString];
    if ([str hasPrefix:@"#"]) {
        str = [str substringFromIndex:1];
    } else if ([str hasPrefix:@"0X"]) {
        str = [str substringFromIndex:2];
    }

    NSUInteger length = [str length];
    //         RGB            RGBA          RRGGBB        RRGGBBAA
    if (length != 3 && length != 4 && length != 6 && length != 8) {
        return NO;
    }

    //RGB,RGBA,RRGGBB,RRGGBBAA
    if (length < 5) {
        *r = ntes_hexStrToInt([str substringWithRange:NSMakeRange(0, 1)]) / 255.0f;
        *g = ntes_hexStrToInt([str substringWithRange:NSMakeRange(1, 1)]) / 255.0f;
        *b = ntes_hexStrToInt([str substringWithRange:NSMakeRange(2, 1)]) / 255.0f;
        if (length == 4) {
            *a = ntes_hexStrToInt([str substringWithRange:NSMakeRange(3, 1)]) / 255.0f;
        } else {
            *a = 1;
        }
    } else {
        *r = ntes_hexStrToInt([str substringWithRange:NSMakeRange(0, 2)]) / 255.0f;
        *g = ntes_hexStrToInt([str substringWithRange:NSMakeRange(2, 2)]) / 255.0f;
        *b = ntes_hexStrToInt([str substringWithRange:NSMakeRange(4, 2)]) / 255.0f;
        if (length == 8) {
            *a = ntes_hexStrToInt([str substringWithRange:NSMakeRange(6, 2)]) / 255.0f;
        } else {
            *a = 1;
        }
    }
    return YES;
}

static inline NSUInteger ntes_hexStrToInt(NSString *str) {
    uint32_t result = 0;
    sscanf([str UTF8String], "%X", &result);
    return result;
}

- (UIImage *)loadImageWithName:(NSString *)name {
    UIImage *image = [UIImage imageNamed:name];
    return image;
}

@end




